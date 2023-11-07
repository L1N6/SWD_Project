using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWD392_EventManagement.IRepository;
using SWD392_EventManagement.Models;

namespace SWD392_EventManagement.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IRepository<Comment, long> _commentRepository;
        private readonly IRepository<Account, long> _accountRepository;
        private readonly IRepository<Event, long> _eventRepository;

        public CommentsController(IRepository<Comment, long> commentRepository, IRepository<Account, long> accountRepository, IRepository<Event, long> eventRepository)
        {
            _commentRepository = commentRepository;
            _accountRepository = accountRepository;
            _eventRepository = eventRepository;
        }

        public async Task<IActionResult> Index()

        {
            var comments = _commentRepository.GetAll();
            return View(comments);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _commentRepository.GetById(id.Value);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_accountRepository.GetAll(), "AccountId", "AccountId");
            ViewData["EventId"] = new SelectList(_eventRepository.GetAll(), "EventId", "EventId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,AccountId,EventId,CreatedAt,DeleteOrUpdate")] Comment comment)
        {

            _commentRepository.Create(comment);
            return RedirectToAction(nameof(Index));



            ViewData["AccountId"] = new SelectList(_accountRepository.GetAll(), "AccountId", "AccountId", comment.AccountId);
            ViewData["EventId"] = new SelectList(_eventRepository.GetAll(), "EventId", "EventId", comment.EventId);
            return View(comment);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _commentRepository.GetById(id.Value);

            if (comment == null)
            {
                return NotFound();
            }

            ViewData["AccountId"] = new SelectList(_accountRepository.GetAll(), "AccountId", "AccountId", comment.AccountId);
            ViewData["EventId"] = new SelectList(_eventRepository.GetAll(), "EventId", "EventId", comment.EventId);
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CommentId,Content,AccountId,EventId,CreatedAt,DeleteOrUpdate")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }


            _commentRepository.Update(comment);
            return RedirectToAction(nameof(Index));


            ViewData["AccountId"] = new SelectList(_accountRepository.GetAll(), "AccountId", "AccountId", comment.AccountId);
            ViewData["EventId"] = new SelectList(_eventRepository.GetAll(), "EventId", "EventId", comment.EventId);
            return View(comment);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _commentRepository.GetById(id.Value);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var comment = _commentRepository.GetById(id);

            if (comment != null)
            {
                _commentRepository.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }


}
