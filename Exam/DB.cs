namespace Exam
{
    public partial class DB
    {
        /// <summary>
        /// Must be initialized first, stores Table Adapters list
        /// </summary>
        public static System.Collections.Hashtable hsTA;

        /// <summary>
        /// Must be initialized first
        /// </summary>
        public static DBTableAdapters.TableAdapterManager TAMQA;
        public static DBTableAdapters.TableAdapterManager TAM;

        /// <summary>
        /// Initializer of TAs, for the given DataSet
        /// </summary>
        /// <param name="dB"></param>
        public static void SetTAM(ref DB dB)
        {
            TAM = new DBTableAdapters.TableAdapterManager();
            TAMQA = new DBTableAdapters.TableAdapterManager();
            hsTA = new System.Collections.Hashtable();

            TAMQA.AnswersTableAdapter = new DBTableAdapters.AnswersTableAdapter();
            TAM.BackupDataSetBeforeUpdate = false;
            TAMQA.BackupDataSetBeforeUpdate = false;
            TAM.ExamsListTableAdapter = new DBTableAdapters.ExamsListTableAdapter();
            TAM.ExamsTableAdapter = new DBTableAdapters.ExamsTableAdapter();
            TAM.OrderTableAdapter = null; // new DBTableAdapters.OrderTableAdapter();
            TAM.PreferencesTableAdapter = new DBTableAdapters.PreferencesTableAdapter();
            TAMQA.QuestionsTableAdapter = new DBTableAdapters.QuestionsTableAdapter();
            TAM.StudentTableAdapter = new DBTableAdapters.StudentTableAdapter();
            TAM.StuListTableAdapter = new DBTableAdapters.StuListTableAdapter();
            TAM.UpdateOrder = DBTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            TAMQA.UpdateOrder = DBTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;

            hsTA.Add(dB.Answers.TableName, TAMQA.AnswersTableAdapter);
            hsTA.Add(dB.Exams.TableName, TAM.ExamsTableAdapter);
            hsTA.Add(dB.ExamsList.TableName, TAM.ExamsListTableAdapter);
            hsTA.Add(dB.Preferences.TableName, TAM.PreferencesTableAdapter);
            hsTA.Add(dB.Questions.TableName, TAMQA.QuestionsTableAdapter);
            hsTA.Add(dB.Student.TableName, TAM.StudentTableAdapter);
            hsTA.Add(dB.StuList.TableName, TAM.StuListTableAdapter);

            foreach (dynamic i in hsTA.Values) //use as dynamic because I cannot find the Class
            {
                i.ClearBeforeFill = true;
            }
        }
    }
}