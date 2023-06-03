using LearningCourses.Data;
using LearningCourses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearningCourses.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tests = _context.Tests.ToList();
            return View(tests);
        }
        public IActionResult Tests(int materialId)
        {
            var tests = _context.Tests
                .Where(t => t.MaterialId == materialId)
                .ToList();

            return View(tests);
        }


        [HttpGet]
        public IActionResult CreateTest(int materialId)
        {
            ViewData["materialId"] = materialId;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateTest(Tests tests, int materialId)
        {
            if (ModelState.IsValid)
            {
                _context.Tests.Add(tests);
                tests.MaterialId = materialId;
                await _context.SaveChangesAsync();

                return RedirectToAction("Index","Home");
            }

            return View(tests);
        }
      
        [HttpGet]
        public IActionResult AddQuestion(int testId)
        {
            var model = new QuestionViewModel
            {
                TestId = testId,
                Answers = new List<AnswerViewModel> { new AnswerViewModel(), new AnswerViewModel(), new AnswerViewModel(), new AnswerViewModel() }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveQuestion(QuestionViewModel model, string action)
        {
            if (ModelState.IsValid)
            {
                var question = new Questions
                {
                    TestId = model.TestId,
                    Content = model.Content
                };

                _context.Questions.Add(question);
                _context.SaveChanges();

                foreach (var answer in model.Answers)
                {
                    var answerEntity = new Answers
                    {
                        QuestionId = question.QuestionId,
                        Content = answer.Content,
                        IsCorrect = answer.IsCorrect ? "true" : "false"
                    };

                    _context.Answers.Add(answerEntity);
                }

                _context.SaveChanges();
            }

            if (action == "continue")
            {
                model.Content = string.Empty;
                model.Answers = new List<AnswerViewModel> { new AnswerViewModel(), new AnswerViewModel(), new AnswerViewModel(), new AnswerViewModel() };
                return View("AddQuestion", model);
            }

            return RedirectToAction("Index");
        }


    }

}
