# Dear DBA: A silly idea

We have fun on the Entity Framework team. Over the years, we've had some pretty ridiculous ideas of things we could (but never would) do. I thought it would be fun to share one of those silly ideas with you.

The year was 2011. We were just about to release version 4.3 which was the first to include Code First Migrations. We were thinking about how you might want to extend the Migrations SQL generation. We were also constantly thinking about how much DBAs hated us because of the convoluted SQL our queries would sometimes generate. (Don't worry, we've since improved that.) We decided that instead of handing the DBAs a SQL script generated by Migrations, it might be better if you could just send a database creation request that described the application's requirements. That way, *they* could write the SQL exactly the way they want it and couldn't complain about how we generated ours.

Well, last night, I decided to have some fun and threw together a prototype. I present to you Dear DBA! It's a Migrations SQL generator that, instead of generating SQL, generates a friendly message you can send to your DBA instead. Here's an example of what it generates for the classic Blogs and Posts model.

> Dear DBA,
>
> We lowly developers would once again petition you for a new database.
>
> We'll need a Blogs table with the following columns. A required Id column to store unique INTEGER values. We'll use the Id value as the primary way of identifying rows in the table. A required Url column to store TEXT values.
>
> We'll also need a Posts table with the following columns. A required Id column to store unique INTEGER values. We'll use the Id value as the primary way of identifying rows in the table. A required Title column to store TEXT values. A required Content column to store TEXT values. A required BlogId column to store INTEGER values that reference the Blogs table.
>
> We don't really know what an index is, but in the past, you've been able to use them to improve performance and compensate for some of our more incompetent queries. We will, of course, defer to your far superior expertise on this matter. Nevertheless, we will suggest a few that we're reasonably certain about.
>
> We think the BlogId column on the Posts table should be indexed.
>
> Sincerely,  
> The Developers

Hopefully my geeky, self-deprecating sense of humor can bring you a bit of cheer. Happy coding!
