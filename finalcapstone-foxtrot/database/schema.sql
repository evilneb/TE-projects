-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

--drop table storage_stability;
--drop table beads_stability;
--drop table chem_compatibility;
--drop table datasets;
--drop table partner_users;
--drop table partners;
--drop table roles;
--drop table users;


create table users
(
	username varchar(32) NOT NULL,     -- Username
	password varchar(32) NOT NULL,      -- Password (in plain-text)

	CONSTRAINT pk_users_username PRIMARY KEY (username)
);

create table roles
(
	username varchar(32) NOT NULL,		-- Username
	technician bit default 0,
	researcher bit default 0,
	partner bit default 0,
	administrator bit default 0,

	constraint fk_roles_username FOREIGN KEY (username) references users (username),
	constraint pk_roles_username PRIMARY KEY (username)
);

create table partners
(
	partner varchar(32) NOT NULL,	-- Company Partner

	constraint pk_partners_partner PRIMARY KEY (partner)
);

create table partner_users
(
	partner varchar(32) NOT NULL,  --Company Partner
	username varchar(32) NOT NULL,

	constraint fk_partner_users_partner_id FOREIGN KEY (partner) references partners (partner),
);

create table datasets
(
	id int identity(1,1) NOT NULL,
	experiment_type varchar(32) NOT NULL,
	created_date date NOT NULL, 
	partner varchar(32),
	formulation_description varchar(256),
	notes text

	constraint pk_datasets_id PRIMARY KEY (id),
);

create table storage_stability
(
	id int identity(1,1) NOT NULL,
	dataset_id int NOT NULL,
	strain varchar(32) NOT NULL,
	form_trt varchar(32) NOT NULL,
	rep varchar(32) NOT NULL,
	preactivation_age varchar(32) NOT NULL,
	temp varchar(32) NOT NULL,
	dpa varchar(32) NOT NULL,
	notes varchar(256),
	dil_zero varchar(32),
	dil_neg1 varchar(32),
	dil_neg2 varchar(32),
	dil_neg3 varchar(32),
	dil_neg4 varchar(32),
	dil_neg5 varchar(32),
	dil_neg6 varchar(32),
	dil_neg7 varchar(32),
	outlier bit default 0

	constraint pk_storage_stability_id PRIMARY KEY (id),
	constraint storage_stability_dataset_id FOREIGN KEY (dataset_id) references datasets (id)
);

create table beads_stability
(
	id int identity(1,1) NOT NULL,
	dataset_id int NOT NULL,
	strain varchar(32) NOT NULL,
	form_trt varchar(32) NOT NULL,
	rep varchar(32) NOT NULL,
	bead_age varchar(32) NOT NULL,
	notes varchar(256),
	dil_1HR_zero varchar(32),
	dil_1HR_neg1 varchar(32),
	dil_1HR_neg2 varchar(32),
	dil_1HR_neg3 varchar(32),
	dil_1HR_neg4 varchar(32),
	dil_1HR_neg5 varchar(32),
	dil_1HR_neg6 varchar(32),
	dil_1HR_neg7 varchar(32),
	dil_6HR_zero varchar(32),
	dil_6HR_neg1 varchar(32),
	dil_6HR_neg2 varchar(32),
	dil_6HR_neg3 varchar(32),
	dil_6HR_neg4 varchar(32),
	dil_6HR_neg5 varchar(32),
	dil_6HR_neg6 varchar(32),
	dil_6HR_neg7 varchar(32),
	dil_6HR_neg8 varchar(32),
	dil_24HR_zero varchar(32),
	dil_24HR_neg1 varchar(32),
	dil_24HR_neg2 varchar(32),
	dil_24HR_neg3 varchar(32),
	dil_24HR_neg4 varchar(32),
	dil_24HR_neg5 varchar(32),
	dil_24HR_neg6 varchar(32),
	dil_24HR_neg7 varchar(32),
	outlier bit default 0

	constraint pk_beads_stability_id PRIMARY KEY (id),
	constraint pk_beads_stability_dataset_id FOREIGN KEY (dataset_id) references datasets (id)
);

create table chem_compatibility
(
	id int identity(1,1) NOT NULL,
	dataset_id int NOT NULL,                    
	strain varchar(32) NOT NULL,
	chemical varchar(32) NOT NULL,
	rep varchar(32) NOT NULL,
	rate_dilution varchar(32) NOT NULL,
	notes varchar(256),
	dil_zero varchar(32),
	dil_neg1 varchar(32),
	dil_neg2 varchar(32),
	dil_neg3 varchar(32),
	dil_neg4 varchar(32),
	dil_neg5 varchar(32),
	dil_neg6 varchar(32),
	dil_neg7 varchar(32),
	outlier bit default 0

	constraint pk_chem_compatibility PRIMARY KEY (id),
	constraint fk_chem_compatibility_dataset_id FOREIGN KEY (dataset_id) references datasets (id)
);

