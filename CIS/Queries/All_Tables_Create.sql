--Summary:
--A table for Accounts

if OBJECT_ID('Contracts') is null
	begin
	create table Contracts(
		ID int not null,
		StartDate datetime default GETDATE(),
		EndDate datetime default GETDATE(),
		Contract_Description varchar(max) null,
		Contract_Status varchar(50),			
		Contract_Offer varchar(max) null,

		EncBy int null,
		EncDate datetime default GETDATE(),
		ModifiedBy int null,
		ModifiedDate datetime default GETDATE()
	)
	print 'Table Contracts succesfully created'
	end
else
	begin
		print 'Table Contracts already exists'
	end

--Summary:
--A table for Signatories

if OBJECT_ID('Signatories') is null
	begin
	create table Signatories(
		ID int not null,
		FullName varchar(max) null,

		EncBy int null,
		EncDate datetime default GETDATE(),
		ModifiedBy int null,
		ModifiedDate datetime default GETDATE()
	)
	print 'Table Signatories succesfully created'
	end
else
	begin
		print 'Table Signatories already exists'
	end

--Summary:
--A table for Contracts_Signatories

if OBJECT_ID('Contracts_Signatories') is null
	begin
	create table Contracts_Signatories(
		ContractID int null,
		SignatoryID int null,

		EncBy int null,
		EncDate datetime default GETDATE(),
		ModifiedBy int null,
		ModifiedDate datetime default GETDATE()
	)
	print 'Table Contracts_Signatories succesfully created'
	end
else
	begin
		print 'Table Contracts_Signatories already exists'
	end

--Summary:
--A table for Accounts

if OBJECT_ID('Accounts') is null
	begin
	create table Accounts(
		ID int not null,
		HolderID int null,
		HolderType int null,		--Options: Person | Firm | Department | Office | Others 
		HolderName varchar(max) null,
		StartDate datetime default GETDATE(),
		EndDate datetime default GETDATE(),

		EncBy int null,
		EncDate datetime default GETDATE(),
		ModifiedBy int null,
		ModifiedDate datetime default GETDATE()
	)
	print 'Table Accounts succesfully created'
	end
else
	begin
		print 'Table Accounts already exists'
	end


--Summary:
--A table for NetworkPlans

if OBJECT_ID('NetworkPlans') is null
	begin
	create table NetworkPlans(
		ID int not null,
		NetworkID int null,
		Plan_Description varchar(max) null,
		Plan_Combo varchar(max) null,
		Plan_Booster varchar(max) null,
		Duration datetime null,

		EncBy int null,
		EncDate datetime default GETDATE(),
		ModifiedBy int null,
		ModifiedDate datetime default GETDATE()
	)
	print 'Table NetworkPlans succesfully created'
	end
else
	begin
		print 'Table NetworkPlans already exists'
	end

--Summary:
--A table for Handsets

if OBJECT_ID('Handsets') is null
	begin
	create table Handsets(
		ID int not null,
		HolderID int null,		--might have same holder id but have to look at holdertype which has corresponding database for identification
		HolderType	int null,	--Options: Person | Firm | Department | Office | Others --wil identify at what table to look at when queryig
		Model int null,

		EncBy int null,
		EncDate datetime default GETDATE(),
		ModifiedBy int null,
		ModifiedDate datetime default GETDATE()
	)
	print 'Table Handsets succesfully created'
	end
else
	begin
		print 'Table Handsets already exists'
	end

if OBJECT_ID('BinCard') is null
	begin
	create table Handsets(
		ID int not null,
		HolderID int null,
		HolderType	int null,	--Options: Person | Firm | Department | Office | Others --wil identify at what table to look at when queryig
		Model int null,

		EncBy int null,
		EncDate datetime default GETDATE(),
		ModifiedBy int null,
		ModifiedDate datetime default GETDATE()
	)
	print 'Table Handsets succesfully created'
	end
else
	begin
		print 'Table Handsets already exists'
	end

--Summary:
--A table for Contracts and NetworkPlans for many-to-many relationship

if OBJECT_ID('Contracts_NetworkPlans') is null
	begin
	create table Contracts_NetworkPlans(
		ID int not null,
		ContractID int null,
		NetworkPlanID int null,

		EncBy int null,
		EncDate datetime default GETDATE(),
		ModifiedBy int null,
		ModifiedDate datetime default GETDATE(),
	)
	print 'Table Contracts_NetworkPlans succesfully created'
	end
else
	begin
		print 'Table Contracts_NetworkPlans already exists'
	end

--Summary:
--A table for non-existing objects

if OBJECT_ID('NonDBObjects') is null
	begin
	create table NonDBObjects(
		ID int not null,
		Name varchar(max) null,
		Remarks varchar(max) null,
		EncBy int null,
		EncDate datetime default GETDATE(),
		ModifiedBy int null,
		ModifiedDate datetime default GETDATE()
	)
	print 'Table NonDBObjects succesfully created'
	end
else
	begin
		print 'Table NonDBObjects already exists'
	end

