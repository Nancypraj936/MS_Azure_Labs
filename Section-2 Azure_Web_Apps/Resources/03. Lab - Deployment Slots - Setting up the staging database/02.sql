ALTER TABLE dbo.CourseOrders ADD Notes NVARCHAR(200)NULL;

INSERT INTO dbo.CourseOrders (CustomerName, CustomerEmail, CourseName, Amount, Notes)
VALUES
('Customer 006', 'customer006@cloudxeus.com', 'AZ-204: Azure Developer Associate', 49.00, 'Bought during weekend promo'),
('Customer 007', 'customer007@cloudxeus.com', 'DP-600: Fabric Analytics Engineer', 59.00, 'Requested invoice'),
('Customer 008', 'customer008@cloudxeus.com', 'AI-900: Azure AI Fundamentals', 19.00, 'First-time learner');
