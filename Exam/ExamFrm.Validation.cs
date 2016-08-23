using System;
using System.Collections.Generic;
using System.Linq;
using Exam.Properties;
using Rsx;

namespace Exam
{
    public partial class ExamFrm
    {
        private string password = "000015325971";
        private DB.ExamsListRow currentExam = null;

        // private string currenStr = string.Empty;

        private DB.StudentRow currentStudent = null;

    
        private System.Drawing.Image image = null;
     
        //pero tiene que comparar si el examen es distinto o el mismo que se quiere volver a corregir.....
        private delegate void UpdateControls();


        private void CarneValidation(string valu)
        {
        
         
         
            this.currentStudent = null;

            Tools.StripAnswer(ref valu);

            if (string.IsNullOrEmpty(valu)) return;

            if (string.IsNullOrWhiteSpace(valu)) return;

            this.statuslbl.Text = Properties.Resources.BuscandoEstudiante;
           
           try
           {
                   this.dB.Student.Clear();
               
                    this.dB.StuList.Clear(); //kjust in case
                //fill student list with studentID equal to carne, all classes materias
                    DB.TAM.StuListTableAdapter.FillByStudentID(this.dB.StuList, valu); //search names from the database
                    int r = this.dB.StuList.Count; //returns 1 row

                    if (r == 0)
                    {
                        this.statuslbl.Text = Resources.NoEnListas;
                        return;
                    }
           
                        //fill students table with all exams from the student! all classes materias
                        DB.TAM.StudentTableAdapter.FillByStudentID(this.dB.Student, valu);


                        currentStudent = new DB.StudentDataTable().NewStudentRow(); //crea un objeto nuevo para guardar info
                        //pero no lo guardes en la tabla
                     //   Tools.StripAnswer(ref )
                        currentStudent.StudentID = valu;
               //NEEDS CLASS MATERIA TO BE COMPLETE

                        //   stu.Name = r.FirstNames.ToUpper();//despues
                        //  stu.Surname = r.LastNames.ToUpper();
                   
                      //  currentStudent = stu; //yes, but do not add to the table yet
                        this.statuslbl.Text = Resources.EstudianteEncontrado;

     
            }
            catch (Exception ex)
            {
                SetStatusException(ref ex);
            }
         
         
        }

      //  private string res = string.Empty;

        private void AssociateExamOrStudent() //tiene que ser toda una funcion
        {

            try
            {
              
                string res = this.ucScan.Result; //sumale por si manda el codigo QR en 2 partes
                res.Trim();
                if (string.IsNullOrEmpty(res))
                {
                    this.statuslbl.Text = "Texto escaneado vacío";
                    return;
                }
            
                this.statuslbl.Text = ucScan.Status; //no se si funcionaria
             
           

                bool IDScanned = res.Contains('*');
                if (IDScanned) //but if it was scanned as a StuID and Answer...
                {

                    string answerRAW = string.Empty; //answer already in the box (typed manually)

                    string stuID = string.Empty; //use as default the carnebox Text
         
                    string[] StuIDAnswer = res.Split('*');
                    stuID = StuIDAnswer[0];
                    answerRAW = StuIDAnswer[1];
                    res = string.Empty; //IMPORTANTISIMO RESETEA LA VARIABLE GLOBAL!!! UNA VEZ ACEPTADO EL CARNET*RESPUESTA
                 
                    this.carneBox.Text = string.Empty;
                    this.carneBox.Text = stuID; //executes the secuence for creating the StudentRow
                 
                    this.verBox.Text = answerRAW; //asign answer to control

                    this.currentStudent.LProvided = answerRAW; //assign answer to student
              
                    //  FindByStudentID(stuID); //finds the student.
                }
                else
                {
                     //this.picBox.Image = null;
         
                     bool valid = ValidateExamModel(res);
                     if (valid) res = string.Empty; //IMPORTANTISIMO RESETEA LA VARIABLE GLOBAL!!! UNA VEZ ENCONTRADO EL EXAMEN
                     else return; //el codigo QR está siendo mandando por partes!!!
              
                }
             

                this.picBox.Image = image; //it can be null
            

            }
            catch (Exception ex)
            {
                SetStatusException(ref ex);
            }


            bool evaluated = false;

            this.carneBox.Enabled = false;
            string studeID = this.carneBox.Text;

            try
            {
               evaluated = ValidateExamStudent(); ///then validate the exam!!!
            }       
            catch (Exception ex)
            {
                SetStatusException(ref ex);
            }

            if (evaluated)
            {
                this.carneBox.Text = string.Empty;
                this.carneBox.Text = studeID;
                this.verBox.Text = string.Empty;

            }
            this.carneBox.Enabled = true;

         
        }

        private bool ValidateAnswer(string provided)
        {
            bool ok = false;

            try
            {

               // if (currentStudent == null) return ok;

                provided.ToUpper();
                provided.Trim();

                // if (provided.Length == 0) return;
                if (string.IsNullOrWhiteSpace(provided)) return ok;
                if (string.IsNullOrEmpty(provided)) return ok;

                Tools.StripAnswer(ref provided);

                ok = CompareAnswerLenght(provided.Length);
                //    bool evaluated = !currentStudent.IsScoreNull();
                //  ok = ok && currentStudent.IsScoreNull();
                currentStudent.LProvided = string.Empty;

                if (ok)
                {
                    currentStudent.LProvided = provided;
                    this.statuslbl.Text = "Respuesta suministrada tiene la longitud necesaria";
                }
                else this.statuslbl.Text = Resources.NoEvaluableRespuesta;

            }
            catch (Exception ex)
            {
                SetStatusException(ref ex);
            }

             return ok;
        }

    

        private bool ValidateExamModel(string res)
        {
           
            currentExam = null;
            bool validated = false;
            image = null;

            string decrypto = Rsx.Encryption.AESThenHMAC.SimpleDecryptWithPassword(res, password, 0); //decrypt the QR Code!!! importantisimo

            if (decrypto == null) return validated;

            currentExam = this.dB.ExamsList.FirstOrDefault(o => o.GUID.CompareTo(decrypto) == 0);
            
            if (currentExam != null)
            {
            
                image = Tools.CreateQRCode(res, qrSise);
                validated = true;

             }
            else this.statuslbl.Text = Resources.QRNoEncontrado;
            return validated;
        }

        private void MakeEvaluationList()
        {
            IEnumerable<DB.StuListRow> list = this.dB.StuList;
            int students = list.Count();
            //   IEnumerable<DB.ExamsListRow> exams = this.dB.ExamsList;
            //      int examenes = exams.Count();
            //  int stuperex = students / examenes;

            foreach (DB.StuListRow r in list)
            {
                DB.StudentRow stu = this.dB.Student.NewStudentRow();

                stu.StudentID = r.StudentID;
                stu.Name = r.FirstNames.ToUpper();
                stu.Surname = r.LastNames.ToUpper();
                this.dB.Student.AddStudentRow(stu);
            }

            DB.TAM.StudentTableAdapter.Update(this.dB.Student);

            /*

            IEnumerable<DB.StudentRow> stus = this.dB.Student;

            int contador = 0;

            foreach (DB.ExamsListRow exa in exams)
            {
                IEnumerable<DB.StudentRow> sublist = null;

         //       IEnumerable<DB.StudentRow> stuExam = this.dB.Student;

                sublist = stus.Where(o => o.IsGUIDNull()).Take(stuperex);

                foreach(DB.StudentRow s in sublist)
                {
                    s.GUID = exa.GUID;

                    s.EID = exa.EID;

                    contador++;
                }

            */

            //  stu.EID = ls.EID;
        }

        private bool ValidateExamStudent()
        {
            if (currentStudent == null)
            {
                this.statuslbl.Text = Resources.NoEvaluableEstudiante;
                return false;
            }
            if (currentExam == null)
            {
                this.statuslbl.Text = Resources.NoEvaluableExamen;
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



         
            bool error = !LinkToExam(); //linkea examen al estudiante (IDs)
            if (error) return false;



            bool answerOk = ValidateAnswer(currentStudent.LProvided);
            if (!answerOk)
            {
            
                return false;
            }


            int dEG = 15; // MAX quince dias desde que el Examen fuese Generado...
            //de lo contrario no aceptes ese examen!!!
            error = HasExpired(dEG, currentExam.Time); //expiró o no? el examen??? HACER ESTO EN MANTENIMIENTO!!!
            if (error) return false;


            //rellena tabla con evaluaciones de un estudiante para una materia dada
            DB.TAM.StudentTableAdapter.FillByClassStudentID(this.dB.Student, currentStudent.Class, currentStudent.StudentID);
            error = HasGUIDDuplicates(currentStudent.GUID); //ya hay evaluaciones de este estudiante con ESTE examen?? ...salte
            if (error) return false;

            int dAP = 90; //DAY ACADEMIC PERIOD?? 90 DAYS = 3 MESES
            int SLID = GetStudentListID(dAP, currentStudent.StudentID, currentStudent.Class);
            error = (SLID == 0);
            if (error) return false;

            IEnumerable<DB.StudentRow> diffExams = null;

            diffExams = DifferentExams();

            int minutesInDay = (24 * 60);
            int mLE = 5 * minutesInDay; // minutes LAST EVALUATION STUDENT = 5 days
               
            if (diffExams.Count() != 0)
            {
                //  ha evaluado examen de la materia antes de un cooling de 5 días???...
                //entonces no lo evalúes porque podrian hackear intentando usar otro examen con respuesta perfecta una vez que ya habia sido evaluado
                error = HasRecents(ref diffExams, mLE); // has recent evaluations same materia last 5 days, weird
                if (error) return false;
                //   
                IEnumerable<DB.StudentRow> olders = null;

                olders = HasOlders(ref diffExams, mLE);



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


            //YA ARREGLADO, BUSCA AL ESTUDIANTE DE LA LISTA QUE TENGA MENOS DE 90 DIAS GENERADO
         
       // .LoadDataRow((currentStudent as System.Data.DataRow).ItemArray, System.Data.LoadOption.OverwriteChanges); // NOT TEEEEEEEY
            try
            {
                DB.StudentRow toAdd = this.dB.Student.NewStudentRow();

                toAdd.ItemArray = currentStudent.ItemArray;
                /*
            
                toAdd.StudentID = currentStudent.StudentID;
                toAdd.SLID = SLID;
                toAdd.Class = currentStudent.Class;
                toAdd.DatePresented = DateTime.Now;
                toAdd.ExamsListRow = currentExam;
                toAdd.LProvided = currentStudent.LProvided;
               */

                this.dB.Student.AddStudentRow(toAdd);
                //   
                currentStudent = toAdd; //IMPORTANTE DEVUELVE LA PAPA

           //     DB.TAM.StudentTableAdapter.Update(this.dB.Student);

                this.statuslbl.Text = Resources.ExamenAsociado;
              
                
                EvaluateStudent();

              
            }
            catch(SystemException ex)
            {
                
            }
            

    
            return true;

        }

        private bool HasExpired(int dEG, DateTime examGenerated)
        {
            DateTime ahora = DateTime.Now;
         //   DateTime examGenerated =examDateTime ;
            //horas elapsadas desde que se generó el examen hasta AHORA 
            double diascreado = ahora.Subtract(examGenerated).TotalDays;
            if (diascreado > dEG) //supera 15 dias  
            {
                this.currentExam = null; //desechalo
                this.currentStudent = null;
                this.statuslbl.Text = "Dias transcurridos desde la creación del examen " + diascreado.ToString() + " mayor a " + dEG.ToString();
                return true; //SALTE EL EXAMEN EXPIRO!!!
            }
            return false;
        }
        private void EvaluateStudent()
        {
            try
            {
                string provided = currentStudent.LProvided;

                //       bool matchLenght = CompareAnswerLenght( provided.Length);
                //    if (!matchLenght) return;

                string TrueAns = currentStudent.ExamsListRow.CLAnswer; //take the encripted answer
                TrueAns = Rsx.Encryption.AESThenHMAC.SimpleDecryptWithPassword(TrueAns, password, 0); //decripta
                Tools.StripAnswer(ref TrueAns);

                string[] split = currentStudent.ExamsListRow.LQuestion.Split(sep2);
                double factor = Convert.ToDouble(split[1]);
                string[] weights = split[0].Split(sep);

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
                    foreach (int x in errorArray) error += x.ToString() + sep.ToString();
                    if (error[error.Length - 1].Equals(sep))
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

                //    currentStudent.ExamsListRow.Points = Decimal.Round(Convert.ToDecimal(puntosTotal),3);


                //    this.picBox.Image = null;

                DB.TAM.StudentTableAdapter.Update(this.dB.Student);

              
           
            }
            catch (Exception ex)
            {
                SetStatusException(ref ex);
            }

            this.currentStudent = null; // reset values
            this.currentExam = null; //reset asignments
          

        }

        private int GetStudentListID(int dAP, string Studentid, string clase)
        {

            DB.TAM.StuListTableAdapter.FillByStudentIDClass(this.dB.StuList, Studentid, clase);

            DateTime ahora = DateTime.Now;

            Func<DB.StuListRow, bool> timecomparer = o =>
               {
                   if (o.IsDateNull()) return false;
                   double days = ahora.Subtract(o.Date).TotalDays; //dias periodo academico StuList
                   if (days < dAP) return true; // menos de 90 dias
                   return false; // mas de 90
               };

            DB.StuListRow stu = this.dB.StuList.FirstOrDefault(timecomparer);

            if (stu == null)
            {
                this.currentStudent = null;
                this.currentExam = null;
                this.statuslbl.Text = Resources.NoEnListas;
                return 0; // no le des SLID
            }
            else return stu.SLID; //HAY RECIENTE
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hEG">max hours since examen generated</param>
        /// <returns></returns>
        private bool LinkToExam()
        {
            //LINK STUDENT TO EXAM
            currentStudent.EID = currentExam.EID;
            currentStudent.Class = currentExam.Class;
            currentStudent.QRCode = Tools.imageToByteArray(image);
            currentStudent.GUID = currentExam.GUID;
            return true;
        }
        private  IEnumerable<DB.StudentRow> HasOlders(ref IEnumerable<DB.StudentRow> diffExams, int mLE)
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
        private IEnumerable<DB.StudentRow> DifferentExams()
        {
            //
            Func<DB.StudentRow, bool> differents = o =>
            {
                if (o.IsGUIDNull()) return false;
                if (o.GUID.CompareTo(currentStudent.GUID) == 0) return false;
                return true;
            };

            IEnumerable<DB.StudentRow> different = this.dB.Student.Where(differents);// lista de mismo estudiante, misma materia pero diferentes periodos academicos o diferentes semanas del trimestre??

            return different;
        }

        private bool HasRecents(ref IEnumerable<DB.StudentRow> different, int mLE)
        {

            DateTime ahora = DateTime.Now;

            Func<DB.StudentRow, bool> recents = o =>
            {
                if (o.IsDatePresentedNull()) return false;

                double minutesSince = ahora.Subtract(o.DatePresented).TotalMinutes;
                if (minutesSince < mLE) return true;
                return false;
            };
            IEnumerable<DB.StudentRow> recent = different.Where(recents).ToList(); //examenes  recientemente presentados
            if (recent.Count() != 0)
            {
                this.currentStudent = null; //quitar esto de aqui??? porque si el boton validar falla no me deja evaluar
                this.currentExam = null;
                //el estudiante YA HA SIDO EVALUADO RECIENTEMENTE tal vez ponerlo de primero
                this.statuslbl.Text = "Estudiante evaluado misma materia en menos de " + mLE.ToString() +" minutos";
                return true;
            }
            else return false;
        }

        private bool HasGUIDDuplicates(string guid)
        {
            //
            Func<DB.StudentRow, bool> samers = o =>
            {
                //evaluacion actual tiene mismo examen GUID de una evaluacion ya existente
                if (o.IsGUIDNull()) return false;
                if (o.GUID.CompareTo(guid) == 0) return true;
                return false;
            };

            IEnumerable<DB.StudentRow> same = this.dB.Student.Where(samers).ToList(); //lista de repetidos examenes no hay que hacerle la prueba del tiempo?
            //como acomodo esto???
            if (same.Count() != 0)
            {
                //AQUIIIIII
                this.currentExam = null;
                this.currentStudent = null;
                //el estudiante YA HA SIDO ASOCIADO AL EXAMEN, tal vez ponerlo de primero
                this.statuslbl.Text = Resources.EstudianteEvaluado; // ya evaluado con este mismo examen!!
                // deberia guardar una lista de estos intentos!!!!
                return true; //SALTE
            }
            return false;
        }
    }
}