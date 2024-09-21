using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {

        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);

            if (comment == null)
            {
                return null;
            }

            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject) 
        {
            var comment = _context.Comment.Include(a => a.AppUser).AsQueryable(); // (AsQueryable) to delay sql query(to list) - defer for filteriing/sorting/limit etc 

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                comment = comment.Where(s => s.Stock.Symbol == queryObject.Symbol);
            };
            if (queryObject.IsDecsending == true)
            {
                comment = comment.OrderByDescending(c => c.CreatedOn);
            }
            return await comment.ToListAsync();
        }


       public async Task<List<Comment>> GetAllAsync()
       {
            return await _context.Comment.ToListAsync();
       }
      
        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comment.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comment.FindAsync(id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}