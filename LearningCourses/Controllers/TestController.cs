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
using static System.Net.Mime.MediaTypeNames;

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
        public IActionResult TestResults(int TestId)
        {
            var Grades = _context.Grades
                .Where(t => t.TestId == TestId)
                .Include(t => t.ApplicationUser)

                .ToList();

            return View(Grades);
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

                return RedirectToAction("Index", "Material");
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
        public IActionResult PassTest(List<QuestionViewModel> questionViewModels)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Получаем ID авторизованного пользователя

            int totalQuestions = questionViewModels.Count;
            int totalCorrectAnswers = 0;

            // Вместо переменной "test" получим данные о тесте из БД
            var test = _context.Tests.FirstOrDefault(t => t.TestId == questionViewModels[0].TestId);

            if (test == null)
            {
                return NotFound();
            }

            foreach (var questionViewModel in questionViewModels)
            {
                int totalQuestionAnswers = questionViewModel.Answers.Count;
                int correctQuestionAnswers = questionViewModel.Answers.Count(a => a.IsCorrect != null);
                int selectedCorrectAnswers = questionViewModel.Answers.Count(a => a.IsCorrect != null && a.IsSelected == "1");

                if (selectedCorrectAnswers == correctQuestionAnswers && totalQuestionAnswers > 0)
                {
                    totalCorrectAnswers++;
                    foreach (var answer in questionViewModel.Answers)
                    {
                        if (answer.IsCorrect == "1" && answer.IsSelected == "1")
                        {
                            answer.IsCorrectAnswerSelected = true;
                        }
                    }
                }
            }



            double percentage = (double)totalCorrectAnswers / totalQuestions * 100;

            int gradeValue;
            
             if (percentage <= 59)
            {
                gradeValue = 2;
            }
            else if (percentage <= 74)
            {
                gradeValue = 3;
            }
            else if (percentage <= 89)
            {
                gradeValue = 4;
            }
            else
            {
                gradeValue = 5;
            }

            var grade = new Grades
            {
                GradeValue = gradeValue,
                ApplicationUserId = userId,
                TestId = questionViewModels[0].TestId,
                DateOfPassage = DateTime.Now
            };

            _context.Grades.Add(grade);
            _context.SaveChanges();

            var resultViewModel = new TestResultViewModel
            {
                TestTitle = test.Title,
                CorrectPercentage = percentage,
                CorrectCount = totalCorrectAnswers,
                IncorrectCount = totalQuestions - totalCorrectAnswers,
                Grade = gradeValue
            };

            return View("TestResult", resultViewModel);
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
