CREATE TABLE Product (
	id INT PRIMARY KEY,
	name VARCHAR(50),
	description NVARCHAR(MAX),
	players VARCHAR(10),
	currency VARCHAR(3),
	default_price DECIMAL,
	product_category VARCHAR(36),
	supplier_category VARCHAR(36),
	image VARCHAR(50)
	);

CREATE TABLE ProductCategory (
	id INT PRIMARY KEY,
	name VARCHAR(50),
	description NVARCHAR(MAX)
	);

CREATE TABLE Supplier (
	id INT PRIMARY KEY,
	name VARCHAR(50),
	description NVARCHAR(MAX)
	);

ALTER TABLE Product ADD FOREIGN KEY (id) REFERENCES ProductCategory(id);
ALTER TABLE Product ADD FOREIGN KEY (id) REFERENCES Supplier(id);