using Exam.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace Exam
{
    public class Validator
    {
        private const string SLASH = "\\";
        private static int QRSIZE = 2;
        private static char SEP = '-';
        private static char SEP2 = ',';

        private DB.ExamsListRow currentExam = null;
        private DB.StudentRow currentStudent = null;
        private Interface inter;

        private char SCANNED_SEPARATOR = '@';

        // private string currenStr = string.Empty;
        public Image Image
        {
            get;
            private set;
        }

        public bool AssignStudentAnswer(string answerProvided)
        {
            if (currentStudent == null) return false;
            else
            {
                currentStudent.LProvided = answerProvided;
                return true;
            }
        }

        public string AssignStudent(string old)
        {
            this.currentStudent = null;

            string carne = old;

            Tools.StripAnswer(ref carne);
            if (carne.CompareTo(old) != 0)
            {
                return carne;
            }

            if (string.IsNullOrEmpty(carne)) return old;

            if (string.IsNullOrWhiteSpace(carne)) return old;

            // old = valu;

            inter.Status = Resources.BuscandoEstudiante;

            try
            {
                // inter.IdB.Student.Clear();

                DB.StuListRow stu = inter.IdB.StuList.FirstOrDefault(o => o.StudentID.CompareTo(carne) == 0);
                if (stu == null)
                {
                    inter.Status = Resources.NoEnListas;
                    return carne;
                }
                inter.IBS.StudentsList.Position =   inter.IBS.StudentsList.Find(inter.IdB.StuList.SLIDColumn.ColumnName, stu.SLID);
                //fill students table with all exams from the student! all classes materias
                DB.TAM.StudentTableAdapter.FillByStudentID(inter.IdB.Student, carne);

                //esto es bien????????
                currentStudent = new DB.StudentDataTable().NewStudentRow(); //crea un objeto nuevo para guardar info
                                                                            //pero no lo guardes en la tabla
                                                                            //   Tools.StripAnswer(ref )
                currentStudent.StudentID = carne;
                //NEEDS CLASS MATERIA TO BE COMPLETE
                // currentStudent = stu; //yes, but do not add to the table yet

                inter.Status = Resources.EstudianteEncontrado;

                // if (currentStudent == null) return valu;

                if (currentStudent.IsScoreNull())
                {
                    this.inter.Status = Resources.EstudianteNoEvaluado;
                }
                else
                {
                    this.inter.Status = Resources.EstudianteEvaluado;
                }
            }
            catch (Exception ex)
            {
                setStatusException(ref ex);
            }

            return carne;
        }

        public void GetScannedData(string scanned, ref string stuID, ref string answerRAW)
        {
            string[] StuIDAnswer = scanned.Split(SCANNED_SEPARATOR);
            stuID = StuIDAnswer[0];
            answerRAW = StuIDAnswer[1];
        }

        public bool IsStudentScanned(string scanned)
        {
            return scanned.Contains(SCANNED_SEPARATOR);
        }

        public bool AssignExamModel(string res)
        {
            currentExam = null;
            bool validated = false;
            Image = null;

            string decrypto = Rsx.Encryption.AESThenHMAC.SimpleDecryptWithPassword(res, inter.Password, 0); //decrypt the QR Code!!! importantisimo

            if (decrypto == null) return validated;

            currentExam = inter.IdB.ExamsList.FirstOrDefault(o => o.GUID.CompareTo(decrypto) == 0);

            if (currentExam != null)
            {
                Image = Tools.CreateQRCode(res, QRSIZE);
                validated = true;
            }
            else
            {
                this.inter.Status = Resources.QRNoEncontrado;
            }
            return validated;
        }

        public bool ValidateExamStudent()
        {
            if (currentStudent == null)
            {
                this.inter.Status = Resources.NoEvaluableEstudiante;
                return false;
            }
            if (currentExam == null)
            {
                this.inter.Status = Resources.NoEvaluableExamen;
                return false;
            }

            /*
            if (currentStudent.IsGUIDNull() || currentStudent.IsScoreNull())
            {
                this.statuslbl.Text = Resources.EvaluandoExamen;
            }
            else
            {
                this.statuslbl.Text = Resources.EstudianteEvaluado;
                return false;
            }
            */

            try
            {
                bool error = !linkStudentToExam(); //linkea examen al estudiante (IDs)
                if (error) return false;

                bool answerOk = validateAnswer(currentStudent.LProvided);
                if (!answerOk)
                {
                    //cuando no mide lo mismo deberia rellenar con 0s
                    inter.Status = Resources.NoEvaluableRespuesta;
                    return false;
                }
                else
                {
                    inter.Status = "Respuesta suministrada tiene la longitud necesaria";
                }

                ///////////////////////////////////////

                int dEG = 15; // MAX quince dias desde que el Examen fuese Generado...
                              //de lo contrario no aceptes ese examen!!!
                error = hasExpired(dEG, currentExam.Time); //expiró o no? el examen??? HACER ESTO EN MANTENIMIENTO!!!
                if (error)
                {
                    this.inter.Status = "Dias desde la creación del examen mayor a " + dEG.ToString();

                    return false;
                }
                //rellena tabla con evaluaciones de un estudiante para una materia dada
                DB.TAM.StudentTableAdapter.FillByClassStudentID(this.inter.IdB.Student, currentStudent.Class, currentStudent.StudentID);
                error = hasGUIDDuplicates(currentStudent.GUID); //ya hay evaluaciones de este estudiante con ESTE examen?? ...salte
                if (error)
                {
                    this.inter.Status = Resources.EstudianteEvaluado; // ya evaluado con este mismo examen!!
                    return false;
                }

                ///////////////////////////////////////

                int dAP = 90; //DAY ACADEMIC PERIOD?? 90 DAYS = 3 MESES
                int SLID = getStudentListID(dAP);
                error = (SLID == 0);
                if (error)
                {
                    inter.Status = "En lista mayor a " + dAP.ToString() + " días";
                    return false;
                }

                ///////////////////////////////////////

                IEnumerable<DB.StudentRow> diffExams = null;

                diffExams = differentExams();

              //  int minutesInDay = (24 * 60);
                int mLE = 10; // seconds LAST EVALUATION STUDENT = 5 days

                if (diffExams.Count() != 0)
                {
                    //  ha evaluado examen de la materia antes de un cooling de 5 días???...
                    //entonces no lo evalúes porque podrian hackear intentando usar otro examen con respuesta perfecta una vez que ya habia sido evaluado
                    error = hasRecents(ref diffExams, mLE); // has recent evaluations same materia last 5 days, weird
                    if (error)
                    {
                        this.inter.Status = "Estudiante evaluado misma materia en menos de " + mLE.ToString() + " minutos";
                        return false;
                    }
                    //
                    IEnumerable<DB.StudentRow> olders = null;
                    olders = hasOlders(ref diffExams, mLE);

                    //que hacer con las evaluaciones repetidas mayores a 5 dias?? podrian ser:
                    //A ) EXAMENES DE LA MISMA MATERIA DEL TRIMESTRE, uno por semana academica!!!
                    // B) EXAMENES MISMA MATERIA DE OTRO TRIMESTRE PASADO DONDE EL ESTUDIANTE RASPO LA MATERIA Y POR ESO ESTA VOLVIENDO A VER LA MATERIA PEEEERO
                    //ENTONCES SU SLID ASIGNADO DEBE SER DISTINTO AL ACTUAL QUE ES UN SLID MAS MODERNO
                    // c) EXAMENES ROBADO?

                    //CASO B) NADA, NO IMPORTA PORQUE LA TABLA STULIST CARGO SOLO EL ULTIMO SLID PARA EL ESTUDIANTE
                    //1) EXAMEN VIEJO --> YA EXPIRO // ANULADO ANTES
                    //2) EXAMEN NUEVO, ENTONCES LA TABLA STULIST SOLO TIENE SU SLID NUEVO Y NO IMPORTA
                    //DEJALO COLAR

                    //CASO C>>???
                    //EL EXAMEN ROBADO TIENE QUE SER:
                    //A) NUEVO, PORQUE LOS VIEJOS EXPIRAN! //ANULADOS
                    //B) SI ES NUEVO (NO HA EXPIRADO)

                    //CASO A)
                    if (olders.Count() != 0)
                    {
                        IEnumerable<DB.StudentRow> sameAcaPeriod = null;
                        sameAcaPeriod = olders.Where(o => o.SLID == SLID);
                        //DEJALO COLAR PORQUE PUEDE SER UNO CADA >= 5 DIAS.... EL PROBLEMA ES SI ES CASO C)... ROBADO
                        //COMO SE ??? CUANDO ES ROBADO O SOBRO Y LO AGARRARON PUEDE SER IGUAL Y PUEDE SER DISTINTO!
                    }
                }

                currentStudent.SLID = SLID;
            }
            catch (Exception ex)
            {
                setStatusException(ref ex);
                return false;
            }

            //YA ARREGLADO, BUSCA AL ESTUDIANTE DE LA LISTA QUE TENGA MENOS DE 90 DIAS GENERADO

            try
            {
                DB.StudentRow toAdd = this.inter.IdB.Student.NewStudentRow();
                toAdd.ItemArray = currentStudent.ItemArray;
                this.inter.IdB.Student.AddStudentRow(toAdd);
                currentStudent = toAdd; //IMPORTANTE DEVUELVE LA PAPA

                this.inter.Status = Resources.ExamenAsociado;

                evaluateStudent();

                this.inter.Status = "Estudiante evaluado";
            }
            catch (Exception ex)
            {
                setStatusException(ref ex);
                return false;
            }

            return true;
        }

        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////

        private bool compareAnswerLenght(int lenghtprovided)
        {
            // if (currentStudent.ExamsListRow == null) return false; //quiere decir que no ha sido escaneado

            string TrueAns = currentExam.CLAnswer;
            TrueAns = Rsx.Encryption.AESThenHMAC.SimpleDecryptWithPassword(TrueAns, inter.Password, 0);
            Tools.StripAnswer(ref TrueAns);

            if (lenghtprovided == TrueAns.Length)
            {
                inter.Status = Properties.Resources.Coincide;
                // inter.StatusHandler?.Invoke(null, EventArgs.Empty);
                return true;
            }
            else
            {
                inter.Status = Properties.Resources.NoCoincide;
                // inter.StatusHandler?.Invoke(null, EventArgs.Empty);
            }
            return false;
        }

        private IEnumerable<DB.StudentRow> differentExams()
        {
            //
            Func<DB.StudentRow, bool> differents = o =>
            {
                if (o.IsGUIDNull()) return false;
                if (o.GUID.CompareTo(currentStudent.GUID) == 0) return false;
                return true;
            };

            IEnumerable<DB.StudentRow> different = this.inter.IdB.Student.Where(differents);// lista de mismo estudiante, misma materia pero diferentes periodos academicos o diferentes semanas del trimestre??

            return different;
        }

        private void evaluateStudent()
        {
            string provided = currentStudent.LProvided;

            string TrueAns = currentStudent.ExamsListRow.CLAnswer; //take the encripted answer
            TrueAns = Rsx.Encryption.AESThenHMAC.SimpleDecryptWithPassword(TrueAns, inter.Password, 0); //decripta
            Tools.StripAnswer(ref TrueAns);

            string[] split = currentStudent.ExamsListRow.LQuestion.Split(SEP2);
            double factor = Convert.ToDouble(split[1]);
            string[] weights = split[0].Split(SEP);

            int[] ws = weights.Select(o => Convert.ToInt32(o)).ToArray();

            IList<int> errorArray = new List<int>();

            double puntos = 0;
            int correct = 0;
            int i = 0;
            double puntosTotal = 0;

            for (i = 0; i < provided.Length; i++)
            {
                char c = provided[i];
                char a = TrueAns[i];
                double puntosQuestion = ws[i] * factor;
                puntosTotal += puntosQuestion;
                if (c == a)
                {
                    puntos += puntosQuestion;
                    correct++;
                }
                else if (c.Equals('0')) continue;
                else errorArray.Add(i + 1);
            }

            string error = string.Empty;
            currentStudent.Error = error;

            if (errorArray.Count != 0)
            {
                foreach (int x in errorArray) error += x.ToString() + SEP.ToString();
                if (error[error.Length - 1].Equals(SEP))
                {
                    currentStudent.Error = error.Substring(0, error.Length - 1);
                }
            }

            currentStudent.Score = (double)Decimal.Round(Convert.ToDecimal(puntos), 1);
            currentStudent.Obs = string.Empty; //nada aun
            currentStudent.Correct = correct;

            currentStudent.DatePresented = DateTime.Now;

            //mejorar esto
            //   currentStudent.ExamsListRow.Questions = currentStudent.ExamsListRow.GetExamsRows().Count();
            // currentStudent.ExamsListRow.Points = Decimal.Round(Convert.ToDecimal(puntosTotal),3);
            // this.picBox.Image = null;

            DB.TAM.StudentTableAdapter.Update(this.inter.IdB.Student);

            this.currentStudent = null; // reset values
            this.currentExam = null; //reset asignments
        }

        private int getStudentListID(int dAP)
        {
            // DB.TAM.StuListTableAdapter.FillByStudentIDClass(inter.IdB.StuList, Studentid, clase);

            DateTime ahora = DateTime.Now;

            Func<DB.StuListRow, bool> timecomparer = o =>
            {
                if (o.IsDateNull()) return false;
                double days = ahora.Subtract(o.Date).TotalDays; //dias periodo academico StuList
                if (days < dAP) return true; // menos de 90 dias
                return false; // mas de 90
            };

            DB.StuListRow stu = inter.IdB.StuList.FirstOrDefault(timecomparer);

            if (stu == null)
            {
                this.currentStudent = null;
                this.currentExam = null;

                return 0; // no le des SLID
            }
            else return stu.SLID; //HAY RECIENTE
        }

        private bool hasExpired(int dEG, DateTime examGenerated)
        {
            DateTime ahora = DateTime.Now;
            //   DateTime examGenerated =examDateTime ;
            //horas elapsadas desde que se generó el examen hasta AHORA
            double diascreado = ahora.Subtract(examGenerated).TotalDays;
            if (diascreado > dEG) //supera 15 dias
            {
                this.currentExam = null; //desechalo
                this.currentStudent = null;
                // this.inter.StatusHandler?.Invoke(null, EventArgs.Empty);

                return true; //SALTE EL EXAMEN EXPIRO!!!
            }
            return false;
        }

        private bool hasGUIDDuplicates(string guid)
        {
            //
            Func<DB.StudentRow, bool> samers = o =>
            {
                //evaluacion actual tiene mismo examen GUID de una evaluacion ya existente
                if (o.IsGUIDNull()) return false;
                if (o.GUID.CompareTo(guid) == 0) return true;
                return false;
            };

            IEnumerable<DB.StudentRow> same = this.inter.IdB.Student.Where(samers).ToList(); //lista de repetidos examenes no hay que hacerle la prueba del tiempo?
            //como acomodo esto???
            if (same.Count() != 0)
            {
                //AQUIIIIII
                this.currentExam = null;
                this.currentStudent = null;
                //el estudiante YA HA SIDO ASOCIADO AL EXAMEN, tal vez ponerlo de primero
                //    this.inter.StatusHandler?.Invoke(null, EventArgs.Empty);

                // deberia guardar una lista de estos intentos!!!!
                return true; //SALTE
            }
            return false;
        }

        private IEnumerable<DB.StudentRow> hasOlders(ref IEnumerable<DB.StudentRow> diffExams, int mLE)
        {
            DateTime ahora = DateTime.Now;

            Func<DB.StudentRow, bool> olders = o =>
            {
                if (o.IsDatePresentedNull()) return false;

                double minutesSince = ahora.Subtract(o.DatePresented).TotalMinutes;
                if (minutesSince > mLE) return true;
                return false;
            };

            IEnumerable<DB.StudentRow> Notrecent = diffExams.Where(olders).ToList(); //examenes diferentes y viejos (ya evaluados) O ROBADOS!!!!

            //
            return Notrecent;
        }

        private bool hasRecents(ref IEnumerable<DB.StudentRow> different, int mLE)
        {
            DateTime ahora = DateTime.Now;

            Func<DB.StudentRow, bool> recents = o =>
            {
                if (o.IsDatePresentedNull()) return false;

                double minutesSince = ahora.Subtract(o.DatePresented).TotalSeconds;
                if (minutesSince < mLE) return true;
                return false;
            };
            IEnumerable<DB.StudentRow> recent = different.Where(recents).ToList(); //examenes  recientemente presentados
            if (recent.Count() != 0)
            {
                this.currentStudent = null; //quitar esto de aqui??? porque si el boton validar falla no me deja evaluar
                this.currentExam = null;
                //el estudiante YA HA SIDO EVALUADO RECIENTEMENTE tal vez ponerlo de primero
                //   this.inter.StatusHandler?.Invoke(null, EventArgs.Empty);

                return true;
            }
            else return false;
        }

        /// <summary>
        /// </summary>
        /// <param name="hEG">max hours since examen generated</param>
        /// <returns></returns>
        private bool linkStudentToExam()
        {
            //LINK STUDENT TO EXAM
            currentStudent.EID = currentExam.EID;
            currentStudent.Class = currentExam.Class;
            currentStudent.QRCode = Tools.imageToByteArray(Image);
            currentStudent.GUID = currentExam.GUID;
            return true;
        }

        private void setStatusException(ref Exception ex)
        {
            inter.Status = ex.Message + "\t\t" + ex.StackTrace;
            // inter.StatusHandler?.Invoke(null, EventArgs.Empty);
        }

        private bool validateAnswer(string provided)
        {
            bool ok = false;

            provided.ToUpper();
            provided.Trim();

            if (string.IsNullOrWhiteSpace(provided)) return ok;
            if (string.IsNullOrEmpty(provided)) return ok;

            Tools.StripAnswer(ref provided);

            ok = compareAnswerLenght(provided.Length);

            currentStudent.LProvided = string.Empty;
            if (ok)
            {
                currentStudent.LProvided = provided;
            }

            return ok;
        }

        public Validator(ref Interface interf)
        {
            inter = interf;
            //inter.
        }
    }
}