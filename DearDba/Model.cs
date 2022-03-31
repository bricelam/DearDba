using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

class Context : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
            .UseSqlite("Data Source=DearDba.db")
            .ReplaceService<IMigrationsSqlGenerator, DearDbaSqlGenerator>();
}

class Blog
{
    public int Id { get; set; }
    public string Url { get; set; } = null!;

    public List<Post> Posts { get; } = new();
}

class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;

    public int BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
}
