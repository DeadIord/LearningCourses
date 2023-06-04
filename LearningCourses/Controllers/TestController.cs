using LearningCourses.Data;
using LearningCourses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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

                return RedirectToAction("Index","Material");
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
                        IsCorrect = answer.IsCorrect
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

        [HttpGet]
        public IActionResult PassTest(int testId)
        {
            var test = _context.Tests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefault(t => t.TestId == testId);

            if (test == null)
            {
                return NotFound();
            }

            var questionViewModels = test.Questions.Select(q => new QuestionViewModel
            {
                TestId = test.TestId,
                Content = q.Content,
                Answers = q.Answers.Select(a => new AnswerViewModel
                {
                    Content = a.Content,
                    IsCorrect = a.IsCorrect
                }).ToList()
            }).ToList();

            return View(questionViewModels);
        }
        [HttpPost]
        public async Task<IActionResult> PassTest(List<QuestionViewModel> questions)
        {
            if (questions == null || questions.Count == 0)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Получить ID текущего авторизованного пользователя

            var testId = questions[0].TestId; // ID теста для сохранения оценки

            var totalQuestions = questions.Count;
            var correctAnswers = 0;

            foreach (var question in questions)
            {
                foreach (var answer in question.Answers)
                {
                    if (answer.IsSelected)
                    {
                        var answers = _context.Answers.Where(a => a.QuestionId == question.QuestionId && a.IsCorrect == "1").ToList();
                        if (answers.Any())
                        {
                            correctAnswers++;
                        }
                    }
                }
            }





            var gradeValue = CalculateGradeValue(correctAnswers, totalQuestions);

            var grade = new Grades
            {
                GradeValue = gradeValue,
                ApplicationUserId = userId,
                TestId = testId
            };

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }



        private int CalculateGradeValue(int correctAnswers, int totalQuestions)
        {
            int percentage = ((int)correctAnswers / totalQuestions) * 100;


            if (percentage < 25)
            {
                return 2;
            }
            else if (percentage < 50)
            {
                return 3;
            }
            else if (percentage < 75)
            {
                return 4;
            }
            else
            {
                return 5;
            }
           
        }




        [HttpGet]
        [ActionName("DeleteComputer")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
           

            if (id != null)
            {
                Tests Tests = await _context.Tests.FirstOrDefaultAsync(p => p.TestId == id);
                if (Tests != null)
                    return View(Tests);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Deletet(int? id)
        {
            if (id != null)
            {
                Tests computers = new() { TestId = id.Value };
                _context.Entry(computers).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                return RedirectToAction("Tests");
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Tests computers = new() { TestId = id.Value };
                _context.Entry(computers).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Material");
            }
            return NotFound();
        }

    }

}
