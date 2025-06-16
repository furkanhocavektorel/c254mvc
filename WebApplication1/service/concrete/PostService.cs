using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.context;
using WebApplication1.entity;
using WebApplication1.Models;
using WebApplication1.service.@abstract;

namespace WebApplication1.service.concrete
{
    public class PostService : IPostService
    {

        SocialContext context;
        public PostService()
        {
            context = new SocialContext();
        }

        public List<PostResponseModel> getPosts()
        {
            List<Post> posts = context.Posts.Include("User").ToList();

            List<PostResponseModel> responses = new List<PostResponseModel>();

            foreach (Post post in posts)
            {
                PostResponseModel model = new PostResponseModel();
                model.Title = post.Title;
                model.Desc = post.Content;
                model.Id = post.Id;
                model.UserFullName = post.User.Name +" "+ post.User.Surname;
                responses.Add(model);
            }

            return responses;
        }

        public void savePost(PostSaveModel model)
        {
            Post post = new Post();
            post.Content = model.Desc;
            post.Title = model.Title;
            post.User__Id = model.UserId;
            post.User=context.Users.Find(model.UserId);


            context.Posts.Add(post);
            context.SaveChanges();
            
        }


    }
}
