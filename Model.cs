using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreExample
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasOne<Blog>(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey("BlogId")
                .HasPrincipalKey("Id")
                .IsRequired();
        }
    }

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasMany<Post>(b => b.Posts)
                .WithOne(p => p.Blog)
                .HasForeignKey("BlogId")
                .HasPrincipalKey("Id")
                ;

            builder.HasOne<Post?>(b => b.FeaturedPost)
                .WithOne()
                .HasForeignKey<Blog?>("FeaturedPostId");
        }
    }

    public class AppContext : DbContext
    {
        public DbSet<Blog>? Blogs { get; set; }
        public DbSet<Post>? Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseMySql("Server=localhost;Database=ef_example;User=root;Password=;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }
    }

    public class Blog
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; set; }
        public Guid? FeaturedPostId { get; set; }
        public Post? FeaturedPost { get; set; }

        public Blog()
        {
            Posts = new List<Post>();
        }

        public Blog(string url, List<Post> posts)
        {
            Url = url;
            Posts = posts;
        }

        public void AddFeaturedPost(Post post)
        {
            FeaturedPost = post;
        }
    }

    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }

        public Post()
        {
        }

        public Post(string title, string content, Blog blog)
        {
            Title = title;
            Content = content;
            Blog = blog;
        }
    }
}