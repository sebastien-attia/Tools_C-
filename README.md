The purpose of this repository is to group tools I use for my project in C#.

I am not an expert in C#, but I like to develop in this language.

## Easy DAO

My first tool (in directory innovea.Tools/innovea.Tools/SQL) is a thin layer to generate automatically
a DAO. I know that there is already existing technos in C# like the Entity Framework,
but it seems that easy to use. Top of that, what I need for my project is quite simple.
I only need an easy to:
* map an object to a table, and
* map each field of this object to a
column of this table,
* mark the Primary Key fields (required for the update and the delete),
* generate the insert, update and delete automatically from this mapping,

In the Java world, JPA solves this problem by annotating each field of a class.

Let's do the same thing in C# !
