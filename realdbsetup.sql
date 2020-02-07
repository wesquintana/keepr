USE mysqlkeepr;

CREATE TABLE vaults (
    id int NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    userId VARCHAR(255),
    INDEX userId (userId),  
    PRIMARY KEY (id)
);

CREATE TABLE keeps (
    id int NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    userId VARCHAR(255),
    img VARCHAR(255),
    isPrivate TINYINT,
    views INT DEFAULT 0,
    shares INT DEFAULT 0,
    keeps INT DEFAULT 0,
    INDEX userId (userId),
    PRIMARY KEY (id)
);

CREATE TABLE vaultkeeps (
    id int NOT NULL AUTO_INCREMENT,
    vaultId int NOT NULL,
    keepId int NOT NULL,
    userId VARCHAR(255) NOT NULL,

    PRIMARY KEY (id),
    INDEX (vaultId, keepId),
    INDEX (userId),

    FOREIGN KEY (vaultId)
        REFERENCES vaults(id)
        ON DELETE CASCADE,

    FOREIGN KEY (keepId)
        REFERENCES keeps(id)
        ON DELETE CASCADE
)