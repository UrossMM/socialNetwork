﻿using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using socialNetwork.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Repositories
{
    public class PostRepo : IPostRepo
    {
        private readonly AppDbContext _context;
        private const int PageSize = 3;

        public PostRepo(AppDbContext context)
        {
            _context = context;
        }

        public Comment AddComment(Comment newComm, int postId, int? parentComm = null)
        {
            var newComment = new Comment
            {
                Text = newComm.Text,
                PostId = postId,
                ParentId = parentComm
            };

            _context.Comments.Add(newComment);
            _context.SaveChanges();

            return newComment;
        }

        public List<CommentDTO> CommentComments(int commId)
        {
            var result = _context.Comments.Where(c => c.ParentId == commId).Select(c => new CommentDTO
            {
                Text = c.Text,
            }).ToList();

            return result;
        }

        public Post CreatePost(Post post, int groupId, string userId)
        {
            //da li je osoba koja hoce da doda post u grupi clan te grupe
            var member = _context.GroupUsers.Where(c => c.UserId == userId && c.GroupId == groupId).FirstOrDefault();

            if (member != null)
            {
                var newPost = new Post
                {
                    Content = post.Content,
                    Type = post.Type,
                    GroupId = groupId,
                    UserId = userId
                };

                _context.Posts.Add(newPost);
                _context.SaveChanges();

                return newPost;
            }

            return null;
        }

        public List<string> GetPosts(int groupId, string user, int? pageNumber)
        {
            //sve objave iz jedne grupe
            var allRowsWithGroupId = _context.Posts.Where(p => p.GroupId == groupId).ToList();

            //ako je admin grupe pozvao funkciju on vidi sve objave cak iako ne prati korisnika koji je napisao privatnu objavu!!!!!!!
            var isAdmin = _context.Groups.FirstOrDefault(g => g.AdminId == user && g.Id == groupId);
            if (isAdmin != null)
            {
                return PaginatedList<string>.Create(allRowsWithGroupId.Select(g => g.Content).ToList().AsQueryable(), pageNumber ?? 1, PageSize);
                // return allRowsWithGroupId.Select(g=> g.Content).ToList();
            }
            else
            {
                //iz odgovarajuce grupe izvucemo sve javne objave 
                var resultPosts = allRowsWithGroupId.Where(p => p.Type == "public").Select(p => p.Content).ToList();

                //privatne objave da se filtriraju - treba da se proveri da li onaj ko hoce da vidi objave prati korisnike koji su napisali te privatne objave

                var joinRows = allRowsWithGroupId.Where(p => p.Type == "private").Join(

                    _context.Followings,
                    post => post.UserId,
                    f => f.FollowedId,
                    (post, f) => new { Content = post.Content, FollowerId = f.FollowerId }).ToList();

                var filteredRows = joinRows.Where(p => p.FollowerId == user).Select(p => p.Content).ToList();

                //takodje treba omoguciti da onaj ko zove fju vidi i svoje objave ( ukljuciti i svoje privatne objave!!!) jer ih prethodna naredba ne vraca jer
                // niko ne moze sam sebe da prati
                var myRows = allRowsWithGroupId.Where(p => p.Type == "private" && p.UserId == user).Select(p => p.Content).ToList();

                resultPosts.AddRange(myRows);
                resultPosts.AddRange(filteredRows);

                resultPosts = PaginatedList<string>.Create(resultPosts.AsQueryable(), pageNumber ?? 1, PageSize);

                return resultPosts;
            }

        }

        public List<CommentDTO> PostComments(int postId)
        {
            var result = _context.Comments.Where(c => c.PostId == postId && c.ParentId == null).Select(c => new CommentDTO
            {
                Text = c.Text,
            }).ToList();

            return result;
        }
    }
}
