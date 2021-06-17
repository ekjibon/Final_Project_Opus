using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSLibrary.Models;
using OPUSERP.Areas.HRPMSLibrary.Models.Lang;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Library;
using OPUSERP.HRPMS.Services.Library.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLibrary.Controllers
{
    [Authorize]
    [Area("HRPMSLibrary")]
    public class BookEntryController : Controller
    {
        private readonly LangGenerate<BookLn> _lang;
        private readonly IBookEntryService bookEntryService;
        private readonly IBookAwardService bookAwardService;
        private readonly IBorrowerInfoService borrowerInfoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookEntryController(IHostingEnvironment hostingEnvironment, IBookAwardService bookAwardService, IBookEntryService bookEntryService, IBorrowerInfoService borrowerInfoService, UserManager<ApplicationUser> userManager)
        {
            _lang = new LangGenerate<BookLn>(hostingEnvironment.ContentRootPath);
            this.bookAwardService = bookAwardService;
            this.bookEntryService = bookEntryService;
            this.borrowerInfoService = borrowerInfoService;
            _userManager = userManager;
        }

        // GET: BookEntry
        public async Task<IActionResult> Index(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            var model = new BookEntryViewModel
            {
                fLang = _lang.PerseLang("Book/BookEntryEN.json", "Book/BookEntryBN.json", Request.Cookies["lang"]),
                bookCategories = await bookAwardService.GetBookCategory(),
                bookEntry = await bookEntryService.GetBookEntryById(id)
            };
            if (model.bookEntry == null) model.bookEntry = new BookEntry();

            return View(model);
        }


        // POST: Training/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BookEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.bookCategories = await bookAwardService.GetBookCategory();
                model.bookEntries = await bookEntryService.GetBookEntry();
                model.fLang = _lang.PerseLang("Book/BookEntryEN.json", "Book/BookEntryBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            BookEntry data = new BookEntry
            {
                Id = model.id,
                bookCategoryId = Int32.Parse(model.categoryId),
                bookId = model.bookId,
                workStation = model.workStation,
                price = model.price,
                department = model.department,
                bookName = model.bookName,
                description = model.description,
                writterName = model.writterName,
                sbnNumber = model.sbnNumber,
                publisher = model.publisher,
                publishDate = model.publishDate,
                almiraNo = model.almiraNo,
                editionNo = model.editionNo,
                selfNo  = model.selfNo,
                quantity = model.quantity,
                satus = model.status,
                orgType = org,
                remark = model.remark
            };

            await bookEntryService.SaveBookEntry(data);

            return RedirectToAction(nameof(BookList));
        }

        // GET: Book/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = new BookEntryViewModel
            { 
                fLang = _lang.PerseLang("Book/BookEntryEN.json", "Book/BookEntryBN.json", Request.Cookies["lang"]),
                bookEntry = await bookEntryService.GetBookEntryById(id),
                borrowerInfos=await borrowerInfoService.GetBorrowerInfoByBookId(id)            
            };
            return View(model);
        }

        // GET: Book List
        public async Task<IActionResult> BookList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            var model = new BookEntryViewModel
            {
                fLang = _lang.PerseLang("Book/BookEntryEN.json", "Book/BookEntryBN.json", Request.Cookies["lang"]),
                bookCategories = await bookAwardService.GetBookCategory(),
                bookEntries = await bookEntryService.GetBookEntryByOrg(org)
            };
            return View(model);
        }

        public async Task<IActionResult> BorrowerInfo(int id)
        {
            var model = new BorrowerInfoViewModel
            {   
                bookId = id.ToString(),
                borrowerInfos = await borrowerInfoService.GetBorrowerInfoByBookId(id),
                bookEntryName = await bookEntryService.GetBookNameById(id),
                fLang = _lang.PerseLang("Book/BookEntryEN.json", "Book/BookEntryBN.json", Request.Cookies["lang"])
            };
            if (model.borrowerInfo == null) model.borrowerInfo = new BorrowerInfo();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrowerInfo([FromForm] BorrowerInfoViewModel model)
        {
            //return Json(model);

            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Book/BookEntryEN.json", "Book/BookEntryBN.json", Request.Cookies["lang"]);
                model.borrowerInfos = await borrowerInfoService.GetBorrowerInfoByBookId(Int32.Parse(model.bookId));
                model.bookEntryName = await bookEntryService.GetBookNameById(Int32.Parse(model.bookId));
                return View(model);
            }

            BorrowerInfo data = new BorrowerInfo
            {
                Id = model.id, 
                bookEntryId = Int32.Parse(model.bookId),
                borrowerId = model.employeeId,
                isReturned = 0,
                dateOfBorrow = model.borrowDate,
                noOfCopy = model.noOfCopy,
                dateOfReturn = model.returnDate
            };

            await borrowerInfoService.SaveBorrowerInfo(data);

            return RedirectToAction(nameof(BorrowerInfo));
        }
       
    }
}