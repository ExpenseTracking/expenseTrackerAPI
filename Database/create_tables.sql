-- create tables
CREATE TABLE userRoles (
    roleId INT PRIMARY KEY,
    roleName VARCHAR(10)
);

CREATE TABLE users (
    userId INT PRIMARY KEY,
    username VARCHAR(20) UNIQUE,
    password VARCHAR(20), -- Storing passwords in plaintext; move to hashing and salting in the future if time allows
    email VARCHAR(30),
    roleId INT,
    createdAt DATETIME,
	updatedAt DATETIME,
	deletedAt DATETIME,
	isDeleted BIT,
    FOREIGN KEY (roleId) REFERENCES userRoles(roleId)
);

CREATE TABLE transactionTypes (
    transactionTypeId INT PRIMARY KEY,
    transactionTypeName VARCHAR(20)
);

CREATE TABLE expenses (
    expenseId INT PRIMARY KEY,
    userId INT,
    transactionTypeId INT,
    amount DECIMAL(10, 2),
    date DATETIME,
    description VARCHAR(255),
	createdAt DATETIME,
	updatedAt DATETIME,
	deletedAt DATETIME,
	isDeleted BIT,
    FOREIGN KEY (userId) REFERENCES users(userId),
    FOREIGN KEY (transactionTypeId) REFERENCES transactionTypes(transactionTypeId)
);