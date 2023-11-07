using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWD392_EventManagement.Models;

namespace SWD392_EventManagement.DAO
{
    public class CommentDAO
    {
        private readonly Swd392Project2Context _context;

        public CommentDAO(Swd392Project2Context context)
        {
            _context = context;
        }

        public void CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public Comment GetCommentById(long commentId)
        {
            return _context.Comments.FirstOrDefault(c => c.CommentId == commentId);
        }

        public List<Comment> GetAllCommentsForEvent(long eventId)
        {
            return _context.Comments.Where(c => c.EventId == eventId).ToList();
        }

        public void UpdateComment(Comment updatedComment)
        {
            _context.Entry(updatedComment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteComment(long commentId)
        {
            var comment = _context.Comments.Find(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }
    }
}
