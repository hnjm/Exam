//*********************************************************************
//
//   Product Name : clsFileNaming
//
//************************ COPYRIGHT INFORMATION **********************
//
//   This program is copyright of SCK-CEN Mol Belgium.
//
//*********************************************************************

//************************ IDENTIFICATION *****************************
//
//   Project : clsFileNaming
//
//   Type : class
//
//   File Name : clsFileNaming.cs
//
//   Programmer(s) : Wim De Boeck wim.de.boeck@sckcen.be
//                   
//   Creation Date : 04 september 2009
//
//   Description : see below
//
//   Reference(s) :  none
//
//*********************************************************************

//************************ HISTORY ************************************
//
//   NR  DATE      BY   INTERNAL_REF, COMMENTS & CHANGES (+references)
//
//   1  2009/09/04 WDB  started
//
//*********************************************************************

//************************ PROGRAMMERS HANDBOOK ***********************
//
//   Class : clsFileNaming
//
//   00) INTRODUCTION
//   ----------------
//
//  This class in needed for creating filenames according the lab's rules
//
//   02) INDEX
//   ---------
//   List of all constants, Variables, Functions, Macros//s
//
//   Constants : none yet
//
//   Variables :
//   1) Local variables for this form
//
//   2) Global variables for the project : none
//
//   03) Components, Objects
//   -----------------------
//
//   03) Macro's and Functions : none
//   -------------------------
//
//   04) Description how the program is built
//   -----------------------------------------
//   Short description of program
//
//
//*********************************************************************

//************************ COMPILE DIRECTIVES *************************
//
//   Description of compile directives used for this program
//
//   Language : C#
//
//   Version : 3.0
//
//   Operation System : Windows XP
//
//
//*********************************************************************


using System;
using System.Collections.Generic;
using System.Text;

namespace Sam
{
    class clsFileNaming
    {
        private char[] m_chrNrOfAcq;
        private int m_NrOfCurrentAcq;
        private string m_detector;
        private int m_Position;
        private string m_SubSampleName;
        private string m_FileNaming;
        //private char m_char;

        public clsFileNaming()
        {
            m_chrNrOfAcq = new char[27];

            m_chrNrOfAcq[1] = 'A';
            m_chrNrOfAcq[2] = 'B';
            m_chrNrOfAcq[3] = 'C';
            m_chrNrOfAcq[4] = 'D';
            m_chrNrOfAcq[5] = 'E';
            m_chrNrOfAcq[6] = 'F';
            m_chrNrOfAcq[7] = 'G';
            m_chrNrOfAcq[8] = 'H';
            m_chrNrOfAcq[9] = 'I';
            m_chrNrOfAcq[10] = 'J';
            m_chrNrOfAcq[11] = 'K';
            m_chrNrOfAcq[12] = 'L';
            m_chrNrOfAcq[13] = 'M';
            m_chrNrOfAcq[14] = 'N';
            m_chrNrOfAcq[15] = 'O';
            m_chrNrOfAcq[16] = 'P';
            m_chrNrOfAcq[17] = 'Q';
            m_chrNrOfAcq[18] = 'R';
            m_chrNrOfAcq[19] = 'S';
            m_chrNrOfAcq[20] = 'T';
            m_chrNrOfAcq[21] = 'U';
            m_chrNrOfAcq[22] = 'V';
            m_chrNrOfAcq[23] = 'W';
            m_chrNrOfAcq[24] = 'X';
            m_chrNrOfAcq[25] = 'Y';
            m_chrNrOfAcq[26] = 'Z';
        }

        public string FileNaming()
        {
            m_FileNaming = m_SubSampleName + m_detector[0] + m_Position.ToString() + m_chrNrOfAcq[m_NrOfCurrentAcq];
            return m_FileNaming;
        }

        public int Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public int NumberCurrentAcquisition
        {
            get { return m_NrOfCurrentAcq; }
            set { m_NrOfCurrentAcq = value; }
        }

        public string DetectorName
        {
            get { return m_detector; }
            set { m_detector = value; }
        }

        public string SubSampleName
        {
            get { return m_SubSampleName; }
            set { m_SubSampleName = value; }
        }

        public char NrAcquisitionChar
        {
            get { return m_chrNrOfAcq[m_NrOfCurrentAcq]; }
        }


        
    }
}
