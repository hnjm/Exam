USE [FISUSB]
GO

DELETE FROM [dbo].[Student]
 
WHERE    
 (EID > 550)
 DELETE FROM [dbo].[ExamsList]
 
WHERE    
 (EID > 550)
  DELETE FROM [dbo].[Exams]
 
WHERE    
 (EID > 550)
  
GO


