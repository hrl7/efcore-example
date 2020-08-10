using System;
using System.Collections.Generic;
using System.Linq;

namespace EfCoreExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppContext())
            {
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog(url: "http://blogs.msdn.com/adonet", posts: new List<Post>()));
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs
                    .OrderBy(b => b.Id)
                    .First();

                // Update
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                Post post = new Post(title: "Hello World", content: "I wrote an app using EF Core!", blog: blog);
                Post featuredPost = new Post(title: "new post!", content: "hogehoge featured post", blog: blog);
                blog.Posts.Add(post);

                blog.AddFeaturedPost(featuredPost);
                db.SaveChanges();

                // Delete
                // Console.WriteLine("Delete the blog");
                // db.Remove(blog);
                // db.SaveChanges();
            }
        }
    }
}