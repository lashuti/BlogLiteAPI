﻿using BlogLiteAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;

namespace BlogLiteAPI.DataAccess
{
    public static class BlogEndpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", async (AppDbContext db)
                => await db.Blogs.ToListAsync())
                .Produces<List<Blog>>(StatusCodes.Status200OK);

            app.MapGet("/blogs/{id:int}", async (AppDbContext db, int id)
                => await db.Blogs.FindAsync(id))
                .Produces<Blog>(StatusCodes.Status200OK);

            app.MapPost("/blogs", async (
                AppDbContext db, 
                IS3ImageService s3ImageService,
                ISqsPublisherService sqsPublisherService,
                IFormFile headerImage, 
                [FromForm] string title, 
                [FromForm] string content) =>
            {
                var imageName = DateTime.Now.ToString("yyyyMMddHHmmss") + '-' + headerImage.FileName;

                var s3Response = await s3ImageService.UploadImageAsync(imageName, headerImage);

                var blog = new Blog { Title = title, Content = content, ImageName = imageName, CreatedAt = DateTime.Now };

                await db.Blogs.AddAsync(blog);
                await db.SaveChangesAsync();

                await sqsPublisherService.PublishAsync<Blog>(blog);

                return Results.Created($"/blogs/{blog.Id}", blog);
            }).Produces<Blog>(StatusCodes.Status201Created).DisableAntiforgery();

            #region

            //    app.MapPost("/blog", async(PostRequest request, IAmazonSimpleNotificationService snsClient) =>
            //    {
            //        var topicArn = "arn:aws:sns:your-region:your-account-id:your-topic"; // Replace with your SNS Topic ARN

            //var message = new PublishRequest
            //{
            //    TopicArn = topicArn,
            //    Subject = $"New Blog Post: {request.Title}",
            //    Message = $"Check out the new blog post: {request.Title}\n\n{request.Content}\n\nRead more at: {request.Url}"
            //};

            //await snsClient.PublishAsync(message);

            //        return Results.Ok("Blog post notification sent.");
            //    })
            //    .WithName("UploadBlog")
            //    .WithOpenApi();
            #endregion
        }
    }
}