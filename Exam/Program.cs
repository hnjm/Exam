using System;
using System.Windows.Forms;

namespace Exam
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string path = Application.StartupPath.ToString();

            ExamFrm.Interface = new Exam.Interface(path);
            ExamFrm form = new ExamFrm();
            form.WindowState = FormWindowState.Maximized;
            Application.Run(form);
        }
    }
}