CREATE TABLE "Hospital" (
    "Id" SERIAL PRIMARY KEY NOT NULL,
    "Name" VARCHAR(100) NOT NULL,
    "Address" VARCHAR(255) NOT NULL
);

CREATE TABLE "Department" (
	"Id" SERIAL PRIMARY KEY NOT NULL,
	"Name" VARCHAR(100) NOT NULL
)

CREATE TABLE "Doctor" (
	"Id" SERIAL PRIMARY KEY NOT NULL,
	"FirstName" VARCHAR(50) NOT NULL,
	"LastName" VARCHAR(50) NOT NULL,
	"HospitalId" INTEGER,
	"DepartmentId" INTEGER,
	
	CONSTRAINT "FK_hospital_doctor_id" FOREIGN KEY ("HospitalId") REFERENCES "Hospital" ("Id"),
	CONSTRAINT "FK_department_doctor_id" FOREIGN KEY ("DepartmentId") REFERENCES "Department" ("Id")
	
)

CREATE TABLE "MedicalTechicalStaff" (
	"Id" SERIAL PRIMARY KEY NOT NULL,
	"Name" VARCHAR(50) NOT NULL,
	"HospitalId" INTEGER,
	"DepartmentId" INTEGER,
	
	CONSTRAINT "FK_hospital_medicaltechinicalstaff_id" FOREIGN KEY ("HospitalId") REFERENCES "Hospital" ("Id"),
	CONSTRAINT "FK_department_medicaltechnicalstaff_id" FOREIGN KEY ("DepartmentId") REFERENCES "Department" ("Id")
)

CREATE TABLE "Patient" (
	"Id" SERIAL PRIMARY KEY NOT NULL,
	"FirstName" VARCHAR(50) NOT NULL,
	"LastName" VARCHAR(50) NOT NULL,
	"Age" INTEGER NOT NULL
)

CREATE TABLE "MedicalCheckUp"(
	"Id" SERIAL PRIMARY KEY NOT NULL,
	"PatientId" INTEGER,
	CONSTRAINT "FK_patient_medicalcheckup_id" FOREIGN KEY ("PatientId") REFERENCES "Patient" ("Id")
)

CREATE TABLE "DoctorMedicalCheckUp"(
	"Id" SERIAL PRIMARY KEY,
	"DoctorId" INTEGER NOT NULL,
	"MedicalCheckUpId" INTEGER NOT NULL,
	CONSTRAINT "FK_doctor_doctormedicalcheckup_id" FOREIGN KEY ("DoctorId") REFERENCES "Doctor" ("Id"),
	CONSTRAINT "FK_medicalcheckup_doctormedicalcheckup_id" FOREIGN KEY ("MedicalCheckUpId") REFERENCES "MedicalCheckUp" ("Id")
)

CREATE TABLE "MedicalRecord"(
	"Id" SERIAL PRIMARY KEY NOT NULL,
	"PrescribedDrugs" TEXT NOT NULL
)

INSERT INTO "Hospital" ("Name", "Address") values ('KBC Osijek', 'Ul. Josipa Huttlera 4');
INSERT INTO "Hospital" ("Name", "Address") values ('KBC Split - Firule', ' Spinčićeva ul. 1, 21000, Split');
INSERT INTO "Hospital" ("Name", "Address") values ('Opća bolnica Zadar', 'Ul. Bože Peričića 5, 23000, Zadar');
INSERT INTO "Hospital" ("Name", "Address") values ('Opća bolnica Požega', 'Osječka ul. 107, 34000, Požega, 34000, Požega');
INSERT INTO "Hospital" ("Name", "Address") values ('Opća bolnica Bjelovar', 'Ul. Antuna Mihanovića 8, 43000, Bjelovar');

INSERT INTO "Department" ("Name") values ('Cardiology');
INSERT INTO "Department" ("Name") values ('Gynecology');
INSERT INTO "Department" ("Name") values ('Radiology');

INSERT INTO "Doctor" ("FirstName", "LastName", "HospitalId", "DepartmentId") values ('Pero', 'Peric', 1, 1);
INSERT INTO "Doctor" ("FirstName", "LastName", "HospitalId", "DepartmentId") values ('Marko', 'Budimir', 2, 1);
INSERT INTO "Doctor" ("FirstName", "LastName", "HospitalId", "DepartmentId") values ('Luka', 'Peric', 2, 1);
INSERT INTO "Doctor" ("FirstName", "LastName", "HospitalId", "DepartmentId") values ('Igor', 'Prpic', 2, 1);
INSERT INTO "Doctor" ("FirstName", "LastName", "HospitalId", "DepartmentId") values ('Antun', 'Ivanic', 2, 1);
INSERT INTO "Doctor" ("FirstName", "LastName", "HospitalId", "DepartmentId") values ('Marko', 'Markic', 2, 1);

INSERT INTO "MedicalTechicalStaff" ("Name", "HospitalId", "DepartmentId") values ('Nurses', 1, 2);
INSERT INTO "MedicalTechicalStaff" ("Name", "HospitalId", "DepartmentId") values ('Hospital pharmacists', 2, 2);
INSERT INTO "MedicalTechicalStaff" ("Name", "HospitalId", "DepartmentId") values ('Dietitian', 1, 1);
INSERT INTO "MedicalTechicalStaff" ("Name", "HospitalId", "DepartmentId") values ('Speech–language pathology', 1, 2);
INSERT INTO "MedicalTechicalStaff" ("Name", "HospitalId", "DepartmentId") values ('Interns', 2, 2);

INSERT INTO "Patient" ("FirstName", "LastName", "Age") values ('Mark', 'Markic', 39);
INSERT INTO "Patient" ("FirstName", "LastName", "Age") values ('Ivan', 'Budimir', 45);
INSERT INTO "Patient" ("FirstName", "LastName", "Age") values ('Marko', 'Sasko', 28);

INSERT INTO "MedicalCheckUp" ("PatientId") values (1);
INSERT INTO "MedicalCheckUp" ("PatientId") values (2);

INSERT INTO "DoctorMedicalCheckUp" ("DoctorId", "MedicalCheckUpId") values (1,1);
INSERT INTO "DoctorMedicalCheckUp" ("DoctorId", "MedicalCheckUpId") values (2,2);

INSERT INTO "MedicalRecord" ("PrescribedDrugs") values ('Alprazolam');
INSERT INTO "MedicalRecord" ("PrescribedDrugs") values ('Histrelin');

ALTER TABLE "MedicalCheckUp" ADD COLUMN "Date" TIMESTAMP;



INSERT INTO "MedicalCheckUp" ("PatientId", "Date") values (1, '2024-07-02');
INSERT INTO "MedicalCheckUp" ("PatientId", "Date") values (1, '2024-07-02 04:05:06');

SELECT *
FROM "Doctor"
INNER JOIN "Hospital" ON "Doctor"."HospitalId" = "Hospital"."Id";

SELECT *
FROM "Doctor"
LEFT JOIN "Hospital" ON "Doctor"."HospitalId" = "Hospital"."Id";

SELECT *
FROM "Doctor"
RIGHT JOIN "Hospital" ON "Doctor"."HospitalId" = "Hospital"."Id";

SELECT * FROM "Hospital";

SELECT *
FROM "Doctor"
INNER JOIN "Hospital" ON "Doctor"."HospitalId" = "Hospital"."Id" WHERE "Hospital"."Name" LIKE '%Opća%'

SELECT *
FROM "Doctor"
INNER JOIN "Hospital" ON "Doctor"."HospitalId" = "Hospital"."Id" WHERE "Hospital"."Address" LIKE '%Ul%'

SELECT *
FROM "MedicalTechicalStaff"
INNER JOIN "Department" ON "MedicalTechicalStaff"."DepartmentId" = "Department"."Id";