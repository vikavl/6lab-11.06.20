/*create database lab6_vlasenko_is81;*/
use lab6_vlasenko_is81;

--create tables
create table EMPLOYEE(
	ID int not null unique,
	EmpName varchar(50) not null,
	Position varchar(50) not null,
	Specialty varchar(50) not null,
	Department varchar(50) not null,
	Equiptment varchar(150) not null,
	BankAccount varchar(16) not null unique,
	primary key(ID, BankAccount)
);

create table PRICE_LIST(
	ID int not null unique,
	ProductName varchar(50) not null,
	UnitPrice float not null,
	Amount int not null,
	Term int not null,
	primary key(ID)
);

create table ORDER_(
	ID int not null unique,
	ProductID int not null,
	EmpID int not null,
	EmpAccount varchar(16) not null,
	CustomerAccount varchar(16) not null,
	primary key(ID),
	foreign key(EmpID, EmpAccount) references EMPLOYEE(ID, BankAccount),
	foreign key(ProductID) references PRICE_LIST(ID)
);

--insert main info in tables
insert into EMPLOYEE(ID, EmpName, Position, Specialty, Department, Equiptment, BankAccount)
values
(1, 'Шевченко Харитон Станиславович', 'pos1', 'spec1', 'depart1', 'equipt1, equipt2, equipt3', '1234567890123456'),
(2, 'Титов Данила Викторович', 'pos2', 'spec2', 'depart2', 'equipt1, eqipt4, equipt5', '0123456789123456'),
(3, 'Червоний Эрик Станиславович', 'pos3', 'spec3', 'depart3', 'equipt2, eqipt3, equipt5', '1123456789123456'),
(4, 'Кузьмин Шамиль Ярославович', 'pos4', 'spec4', 'depart1', 'equipt1, eqipt3, equipt4', '1223456789123456');

insert into PRICE_LIST(ID, ProductName, UnitPrice, Amount, Term)
values
(1, 'Книжка', '200', '200', '3'),
(2, 'Книжка', '250', '200', '1'),
(3, 'Журнал', '100', '100', '3'),
(4, 'Журнал', '150', '100', '1'),
(5, 'Газета', '10', '50', '3'),
(6, 'Газета', '12', '50', '1'),
(7, 'Листівка', '5', '100', '3'),
(8, 'Листівка', '7', '100', '1'),
(9, 'Конверт', '3', '100', '3'),
(10, 'Конверт', '5', '100', '1'),
(11, 'Плакат', '25', '100', '3'),
(12, 'Плакат', '30', '100', '1');

insert into ORDER_(ID, ProductID, EmpID, EmpAccount, CustomerAccount)
values
(1, 1, 1, '1234567890123456', '9876543210987654'),
(2, 3, 1, '1234567890123456', '9876543210987654'),
(3, 7, 2, '0123456789123456', '8794561230032165'),
(4, 12, 3, '1123456789123456', '4456789123002151'),
(5, 5, 4, '1223456789123456', '5546987120314569'),
(6, 9, 3, '1123456789123456', '6694841238400054');

--read info from tables
select * from EMPLOYEE;
select * from PRICE_LIST;
select * from ORDER_;

--10 requests to bd
/*1 - Сума за послугу вища за 150 грн*/
select * from PRICE_LIST
where UnitPrice>=150;
/*2 - Вивести виконавців, які виконали більше одного замовлення*/
select count(EMPLOYEE.ID) as Number_Of_Orders, EMPLOYEE.ID, EmpName, Position, Specialty, Department, Equiptment, BankAccount
from EMPLOYEE inner join ORDER_ on EMPLOYEE.ID=ORDER_.EmpID
group by EMPLOYEE.ID, EmpName, Position, Specialty, Department, Equiptment, BankAccount
having count(EMPLOYEE.ID)>1;
/*3 - Вивести покупців (номера банківських рахунків), які робили покупки у видавництві більше одного разу*/
select count(ID) as Number_Of_Orders, CustomerAccount
from ORDER_
group by CustomerAccount
having count(ID)>1;
/*4 - Вивести усю інформацію з плайс-листа про "Листівки"*/
select * from PRICE_LIST
where ProductName like 'Листівка';
/*5 - Вивести усі послуги, які виконуються більше двох днів*/
select * from PRICE_LIST
where Term>2;
/*6 - Знайти співробітників, які працюють з однаковим технічними засобами зі списків*/
select EmpName, Equiptment
from EMPLOYEE
group by EmpName, Equiptment
having count(ID)>1
/*7 - Вивести список департаментів співробітників, що не повторюються????*/
select Department
from EMPLOYEE
where Department in(select Department
	from(
	select Department, count(Department) as uniq
	from EMPLOYEE
	group by Department) t
	where uniq=1
)
/*8 - Вивести імена виконавців та відповідну інформацію про замовлення, що було виконане ними*/
select *
from EMPLOYEE inner join ORDER_ on EMPLOYEE.ID=ORDER_.EmpID
/*9 - Вивести найдешевшу послугу у видавництві*/
select top 1 *
from PRICE_LIST
where UnitPrice>0
order by UnitPrice asc
/*10 - Вивести співробітників та кількість виконаних ними замовлень*/
select count(EMPLOYEE.ID) as Number_Of_Orders, EMPLOYEE.ID, EmpName, Position, Specialty, Department, Equiptment, BankAccount
from EMPLOYEE inner join ORDER_ on EMPLOYEE.ID=ORDER_.EmpID
group by EMPLOYEE.ID, EmpName, Position, Specialty, Department, Equiptment, BankAccount
