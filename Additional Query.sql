--run this query one you run the application
--If you are happy with the work, please rate me and give me a good comment :)  

update AspNetUsers set Names = 'Admin' where UserName = 'admin@ecu.com'
update AspNetUsers set Names = 'Score Manager' where UserName = 'event@ecu.com'

INSERT INTO AspNetUserRoles
VALUES ((SELECT Id FROM AspNetUsers WHERE UserName = 'admin@ecu.com'),(SELECT Id FROM AspNetRoles WHERE Name = 'Admin'))

INSERT INTO AspNetUserRoles
VALUES ((SELECT Id FROM AspNetUsers WHERE UserName = 'event@ecu.com'),(SELECT Id FROM AspNetRoles WHERE Name = 'ScoreManager'))



