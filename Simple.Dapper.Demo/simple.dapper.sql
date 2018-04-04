IF OBJECT_ID('Book','U') IS NOT NULL
DROP TABLE Book
GO
CREATE TABLE Book
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(MAX) NOT NULL	
)

GO
IF	OBJECT_ID('BookReview','U') IS NOT NULL
DROP TABLE BookReview
GO
CREATE TABLE BookReview
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	BookId INT,
	Content TEXT,
	FOREIGN KEY(BookId) REFERENCES Book(Id)	
)
GO

IF OBJECT_ID('proc_generatedata','P') IS NOT NULL
DROP PROC proc_generatedata
GO
CREATE PROC proc_generatedata
AS
	--生成100本书，1W条书评
	begin
		DECLARE @count INT, @row INT
		SET @count = 100
		SET @row = 1
		WHILE	@row <= @count
			BEGIN
				DECLARE @reviewcount INT, @reviewrow INT, @id INT
				SET @reviewcount = 100
				SET @reviewrow = 1
				
				INSERT INTO Book(Name) VALUES('维基逃离MySQL 力挺开源数据库' + cast(ROUND(RAND()*100,0) as nvarchar(max)))
				SELECT @id = SCOPE_IDENTITY()
				
				--循环插入书评
				WHILE @reviewrow <= @reviewcount
					begin
						
						INSERT INTO BookReview(BookId, [Content] ) VALUES (@id,'这本书很好看咯' + cast(ROUND(RAND()*100,0) as nvarchar(max)))
						
						SET @reviewrow = @reviewrow + 1
					end
				SET @row = @row + 1
			END
	end	
	
	
SELECT * FROM Book b

SELECT * FROM BookReview br