using System;

namespace Exam
{
    public class Interface
    {
        private string status;

        public BS IBS { get; private set; }

        public DB IdB { get; private set; }

        public Generator IGenerator { get; private set; }

        public Validator IValidator { get; private set; }

        // private string password = "000015325971";
        public string Password
        {
            get { return "000015325971"; }
        }

        public string Path
        {
            get;
            private set;
        }

        public EventHandler ProgressHandler
        {
            get;
            set;
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                StatusHandler?.Invoke(null, EventArgs.Empty);
            }
        }

        public EventHandler StatusHandler
        {
            get;
            set;
        }

        public Interface(string appPath)
        {
            DB set = new DB();
            IdB = set;
            IBS = new BS(ref set);
            DB.SetTAM(ref set);
            Path = appPath;

            Interface reference = this;

            IGenerator = new Generator(ref reference);
            IValidator = new Validator(ref reference);
        }

        public void Save()
        {
            IBS.EndEdit();

            DB.TAMQA.UpdateAll(IdB);
            DB.TAM.UpdateAll(IdB);
        }
    }
}