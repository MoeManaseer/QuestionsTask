# QuestionsTask

The task is to create a Survey questions configurator application, as in the following requirements:
•	It should be developed using .NET framework as desktop application with SQL server DB.
•	All application configuration should be saved in the DB, so when the application is closed and reopened, it should reflect the latest status.
•	The main screen content should have the following:
o	A list of already created questions (loaded from the DB). The question text for each question should appear in the list.
o	The list should support selection and ordering.
o	Three buttons:
	Add: it will open a dialog form to create a new questions
	Edit: it will open a dialog form to edit the properties of the selected question from the list.
	Delete: it will delete the selected question from the list (after asking the user for confirmation).
•	There should be three types of questions:
o	Smiley faces question. it has the following properties:
	Question text
	Question order
	Number of smiley faces (from 2 to 5)
o	Slider question with the following properties:
	Question text
	Question order
	Start value
	End value (up 100)
	Start value caption
	End value caption
o	Stars question
	Question text
	Question order
	Number of stars (up to 10)
•	All modifications should be reflected directly on the DB. For example, when the user create a question and click save button, the question should be directly inserted to the DB.
•	User experience and design should be constructed to match standard windows applications, and office applications.
•	The application should present helpful messages to the user, and should inform the user for any error happened.
•	User input validations should be implemented when required.
•	The application should not crash due to any user action.
•	Accessing the DB should be done using ADO.NET command. You can NOT use LINQ or Entity framework.
•	The code should have proper amount of comments.
•	All relations should reflected on the DB with validation.
•	The application should support multi-instance running from same PC or different PC. The user should only add the DB connection parameters in a configuration file beside the application.
•	All classes, variables, DB tables should be named informatively and with a convention.
•	All errors should be written in a log file with a proper format.
•	Apply learned knowledge from latest reading in this task, so the code should reflect what you learned.
•	Code Reusability and abstractions are highly important.
•	Create a Github repository and upload the task on it. You should make at least one commits every three hours of work to reflect gradual changes. Once you create the repository, kindly send its link to me (it should be a public one)
•	Create a progress.txt file and put it on the same github repository. The file should include an entry per day, containing the following:
o	Date
o	What is accomplished today.
o	What is remaining to be done (as list of items).
This text file should be updated once at the end of the day.
•	At the end of the task, create a documentation of your work in word format. The documentation should have three sections and each one should target one of these types of audiences:
o	End user who will use the task (it is like user manual)
o	Administrator: the one who will install the task on the user PC.
o	Developer: Someone who will work on your task, so you should document for him all the technical decisions you have made and to explain the design/architecture of your application & DB.
•	Test your application thoroughly and deeply for all strange scenarios until there are no bugs left to be found. 
