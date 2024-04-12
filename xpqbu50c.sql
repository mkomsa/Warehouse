CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Addresses" (
    "Id" uuid NOT NULL,
    "PostalCode" text NOT NULL,
    "Street" text NOT NULL,
    "Apartment" text NOT NULL,
    CONSTRAINT "PK_Addresses" PRIMARY KEY ("Id")
);

CREATE TABLE "Customers" (
    "Id" uuid NOT NULL,
    "AddressId" uuid NOT NULL,
    "Name" text NOT NULL,
    "FullName" text NOT NULL,
    "Email" text NOT NULL,
    "PhoneNumber" text NOT NULL,
    CONSTRAINT "PK_Customers" PRIMARY KEY ("Id")
);

CREATE TABLE "Invoices" (
    "Id" uuid NOT NULL,
    "TransactionDate" timestamp with time zone NOT NULL,
    "NetValue" double precision NOT NULL,
    "GrossValue" double precision NOT NULL,
    "Status" text NOT NULL,
    "VatRate" double precision NOT NULL,
    CONSTRAINT "PK_Invoices" PRIMARY KEY ("Id")
);

CREATE TABLE "Manufacturers" (
    "Id" uuid NOT NULL,
    "AddressId" uuid NOT NULL,
    "Name" text NOT NULL,
    "Email" text NOT NULL,
    "PhoneNumber" text NOT NULL,
    CONSTRAINT "PK_Manufacturers" PRIMARY KEY ("Id")
);

CREATE TABLE "ParcelsInfo" (
    "Id" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "Length" double precision NOT NULL,
    "Width" double precision NOT NULL,
    "Height" double precision NOT NULL,
    "Weight" double precision NOT NULL,
    CONSTRAINT "PK_ParcelsInfo" PRIMARY KEY ("Id")
);

CREATE TABLE "Products" (
    "Id" uuid NOT NULL,
    "CategoryId" uuid NOT NULL,
    "ParcelInfoId" uuid NOT NULL,
    "ManufacturerId" uuid NOT NULL,
    "Price" double precision NOT NULL,
    "AvailableAmount" integer NOT NULL,
    CONSTRAINT "PK_Products" PRIMARY KEY ("Id")
);

CREATE TABLE "Orders" (
    "Id" uuid NOT NULL,
    "CustomerId" uuid NOT NULL,
    "AddressId" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "InvoiceId" uuid NOT NULL,
    CONSTRAINT "PK_Orders" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Orders_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Orders_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Orders_CustomerId" ON "Orders" ("CustomerId");

CREATE INDEX "IX_Orders_ProductId" ON "Orders" ("ProductId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240412151423_Init', '8.0.4');

COMMIT;

