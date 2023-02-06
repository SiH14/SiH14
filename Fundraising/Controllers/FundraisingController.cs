using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fundraising.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Numerics;

namespace Fundraising.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FundraisingController : ControllerBase
    {
        private readonly FundraisingDbContext _context;

        public FundraisingController(FundraisingDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetList()
        //{
        //    return await _context.Products.ToListAsync();
        //}

        //api/Fundraising
        [HttpGet("topage/{topage}/{sele}")]//探索
        public async Task<ActionResult<IEnumerable<dynamic>>> GettopageList(int topage , int sele)
        {
            var addprice = from p in _context.Plans
                           join o in _context.Orders on p.PlanId equals o.PlanId
                           //group p by new {p.ProductId} into g
                           where o.OrderStateId != 4 && o.OrderStateId != 5 && o.OrderStateId != 6
                           select new
                           {
                               ProductId = p.ProductId,
                               toll = (p.PlanPrice + o.AddSponsorship)
                           };

            var sumprice = from add in addprice
                           group add by new { add.ProductId } into g
                           //where p.ProductId == 15
                           select new
                           {
                               ProductId = g.Key.ProductId,
                               toll = g.Sum(ap => ap.toll)
                           };

            var countplan = from p in _context.Plans
                            join o in _context.Orders on p.PlanId equals o.PlanId
                            where o.OrderStateId != 4 && o.OrderStateId != 5 && o.OrderStateId != 6
                            group p by new { p.ProductId } into g
                            select new
                            {
                                ProductId = g.Key.ProductId,
                                Coun = g.Count()
                            };

            var combinesum = from p in _context.Products
                             join sp in sumprice on p.ProductId equals sp.ProductId into ps
                             from sp in ps.DefaultIfEmpty()
                             select new
                             {
                                 ProductId = p.ProductId,
                                 nowmoney = sp.toll == null ? 0 : sp.toll
                             };

            var combine = from o in combinesum
                          join c in countplan on o.ProductId equals c.ProductId into ps
                          from c in ps.DefaultIfEmpty()
                          select new
                          {
                              ProductId = o.ProductId,
                              nowmoney = o.nowmoney,
                              nowperson = c.Coun == null ? 0 : c.Coun
                          };

            var queryy = from o in combine
                        join product in _context.Products on o.ProductId equals product.ProductId
                        join user in _context.Users on product.UserId equals user.UserId
                        where product.ProductStateId == 3
                        orderby o.nowmoney descending
                        select new
                        {
                            ProductId = o.ProductId,
                            UserName = user.UserName,
                            ProductTitle = product.ProductTitle,
                            coverphoto = product.Coverphoto,
                            CurrentAmount = o.nowmoney,
                            TargetAmount = product.TargetAmount,
                            Startime = product.Startime,
                            Endtime = product.Endtime,
                            nowperson = o.nowperson,
                            days = (product.Endtime - DateTime.Now).Days + 1,
                            percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                        };

            if (sele == 1)
            {
                var query = from o in combine
                            join product in _context.Products on o.ProductId equals product.ProductId
                            join user in _context.Users on product.UserId equals user.UserId
                            where product.ProductStateId == 3 && product.Endtime > DateTime.Now
                            orderby (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100) descending
                            select new
                            {
                                ProductId = o.ProductId,
                                UserName = user.UserName,
                                ProductTitle = product.ProductTitle,
                                coverphoto = product.Coverphoto,
                                CurrentAmount = o.nowmoney,
                                TargetAmount = product.TargetAmount,
                                Startime = product.Startime,
                                Endtime = product.Endtime,
                                nowperson = o.nowperson,
                                days = (product.Endtime - DateTime.Now).Days + 1,
                                percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                            };
                query = query.Skip(topage * 6).Take(6);
                return await query.ToListAsync();
            }
            else if (sele == 2)
            {
                var query = from o in combine
                            join product in _context.Products on o.ProductId equals product.ProductId
                            join user in _context.Users on product.UserId equals user.UserId
                            where product.ProductStateId == 3 && product.Endtime > DateTime.Now
                            orderby product.Startime descending
                            select new
                            {
                                ProductId = o.ProductId,
                                UserName = user.UserName,
                                ProductTitle = product.ProductTitle,
                                coverphoto = product.Coverphoto,
                                CurrentAmount = o.nowmoney,
                                TargetAmount = product.TargetAmount,
                                Startime = product.Startime,
                                Endtime = product.Endtime,
                                nowperson = o.nowperson,
                                days = (product.Endtime - DateTime.Now).Days + 1,
                                percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                            };
                query = query.Skip(topage * 6).Take(6);
                return await query.ToListAsync();
            }
            else if (sele == 3)
            {
                var query = from o in combine
                            join product in _context.Products on o.ProductId equals product.ProductId
                            join user in _context.Users on product.UserId equals user.UserId
                            where product.ProductStateId == 3 && product.Endtime > DateTime.Now
                            orderby o.nowmoney descending
                            select new
                            {
                                ProductId = o.ProductId,
                                UserName = user.UserName,
                                ProductTitle = product.ProductTitle,
                                coverphoto = product.Coverphoto,
                                CurrentAmount = o.nowmoney,
                                TargetAmount = product.TargetAmount,
                                Startime = product.Startime,
                                Endtime = product.Endtime,
                                nowperson = o.nowperson,
                                days = (product.Endtime - DateTime.Now).Days + 1,
                                percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                            };
                query = query.Skip(topage * 6).Take(6);
                return await query.ToListAsync();
            }
            else if (sele == 4)
            {
                var query = from o in combine
                            join product in _context.Products on o.ProductId equals product.ProductId
                            join user in _context.Users on product.UserId equals user.UserId
                            where product.ProductStateId == 3 && product.Endtime > DateTime.Now
                            orderby o.nowperson descending
                            select new
                            {
                                ProductId = o.ProductId,
                                UserName = user.UserName,
                                ProductTitle = product.ProductTitle,
                                coverphoto = product.Coverphoto,
                                CurrentAmount = o.nowmoney,
                                TargetAmount = product.TargetAmount,
                                Startime = product.Startime,
                                Endtime = product.Endtime,
                                nowperson = o.nowperson,
                                days = (product.Endtime - DateTime.Now).Days + 1,
                                percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                            };
                query = query.Skip(topage * 6).Take(6);
                return await query.ToListAsync();
            }
            else if (sele == 5)
            {
                var query = from o in combine
                            join product in _context.Products on o.ProductId equals product.ProductId
                            join user in _context.Users on product.UserId equals user.UserId
                            where product.ProductStateId == 3 && product.Endtime < DateTime.Now && o.nowmoney > product.TargetAmount
                            orderby o.nowperson descending
                            select new
                            {
                                ProductId = o.ProductId,
                                UserName = user.UserName,
                                ProductTitle = product.ProductTitle,
                                coverphoto = product.Coverphoto,
                                CurrentAmount = o.nowmoney,
                                TargetAmount = product.TargetAmount,
                                Startime = product.Startime,
                                Endtime = product.Endtime,
                                nowperson = o.nowperson,
                                days = 0,
                                percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                            };
                query = query.Skip(topage * 6).Take(6);
                return await query.ToListAsync();
            }
            queryy = queryy.Skip(topage * 6).Take(6);
            return await queryy.ToListAsync();
        }
        //api/Fundraising
        [HttpGet]//根本
        public async Task<ActionResult<IEnumerable<dynamic>>> GetList()
        {
            var addprice = from p in _context.Plans
                           join o in _context.Orders on p.PlanId equals o.PlanId
                           //group p by new {p.ProductId} into g
                           where o.OrderStateId != 5 && o.OrderStateId != 4
                           select new
                           {
                               ProductId = p.ProductId,
                               toll = (p.PlanPrice + o.AddSponsorship)
                           };

            var sumprice = from add in addprice
                           group add by new { add.ProductId } into g
                           //where p.ProductId == 15
                           select new
                           {
                               ProductId = g.Key.ProductId,
                               toll = g.Sum(ap => ap.toll)
                           };

            var countplan = from p in _context.Plans
                            join o in _context.Orders on p.PlanId equals o.PlanId
                            group p by new { p.ProductId } into g
                            select new
                            {
                                ProductId = g.Key.ProductId,
                                Coun = g.Count()
                            };

            var combinesum = from p in _context.Products
                             join sp in sumprice on p.ProductId equals sp.ProductId into ps
                             from sp in ps.DefaultIfEmpty()
                             select new
                             {
                                 ProductId = p.ProductId,
                                 nowmoney = sp.toll == null ? 0 : sp.toll
                             };

            var combine = from o in combinesum
                          join c in countplan on o.ProductId equals c.ProductId into ps
                          from c in ps.DefaultIfEmpty()
                          select new
                          {
                              ProductId = o.ProductId,
                              nowmoney = o.nowmoney,
                              nowperson = c.Coun == null ? 0 : c.Coun
                          };

            var query = from o in combine
                        join product in _context.Products on o.ProductId equals product.ProductId
                        join user in _context.Users on product.UserId equals user.UserId
                        orderby o.nowmoney descending
                        select new
                        {
                            ProductId = o.ProductId,
                            UserName = user.UserName,
                            ProductTitle = product.ProductTitle,
                            coverphoto = product.Coverphoto,
                            CurrentAmount = o.nowmoney,
                            TargetAmount = product.TargetAmount,
                            Startime = product.Startime,
                            Endtime = product.Endtime,
                            nowperson = o.nowperson,
                            days = (product.Endtime - DateTime.Now).Days + 1,
                            percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                        };
            return await query.ToListAsync();
        }

        [HttpGet("featured")]//精選項目
        public async Task<ActionResult<IEnumerable<dynamic>>> GetfeaturedList()
        {
            var addprice = from p in _context.Plans
                           join o in _context.Orders on p.PlanId equals o.PlanId
                           //group p by new {p.ProductId} into g
                           where o.OrderStateId != 4 && o.OrderStateId != 5 && o.OrderStateId != 6
                           select new
                           {
                               ProductId = p.ProductId,
                               toll = (p.PlanPrice + o.AddSponsorship)
                           };

            var sumprice = from add in addprice
                           group add by new { add.ProductId } into g
                           //where p.ProductId == 15
                           select new
                           {
                               ProductId = g.Key.ProductId,
                               toll = g.Sum(ap => ap.toll)
                           };

            var countplan = from p in _context.Plans
                            join o in _context.Orders on p.PlanId equals o.PlanId
                            where o.OrderStateId != 4 && o.OrderStateId != 5 && o.OrderStateId != 6
                            group p by new { p.ProductId } into g
                            select new
                            {
                                ProductId = g.Key.ProductId,
                                Coun = g.Count()
                            };

            var combinesum = from p in _context.Products
                             join sp in sumprice on p.ProductId equals sp.ProductId into ps
                             from sp in ps.DefaultIfEmpty()
                             select new
                             {
                                 ProductId = p.ProductId,
                                 nowmoney = sp.toll == null ? 0 : sp.toll
                             };

            var combine = from o in combinesum
                          join c in countplan on o.ProductId equals c.ProductId into ps
                          from c in ps.DefaultIfEmpty()
                          select new
                          {
                              ProductId = o.ProductId,
                              nowmoney = o.nowmoney,
                              nowperson = c.Coun == null ? 0 : c.Coun
                          };

            var query = from o in combine
                        join product in _context.Products on o.ProductId equals product.ProductId
                        join user in _context.Users on product.UserId equals user.UserId
                        where product.Featured == true && product.ProductStateId == 3 && product.Endtime > DateTime.Now
                        orderby (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100) descending
                        select new
                        {
                            ProductId = o.ProductId,
                            UserName = user.UserName,
                            ProductTitle = product.ProductTitle,
                            coverphoto = product.Coverphoto,
                            CurrentAmount = o.nowmoney,
                            TargetAmount = product.TargetAmount,
                            Startime = product.Startime,
                            Endtime = product.Endtime,
                            nowperson = o.nowperson,
                            days = (product.Endtime - DateTime.Now).Days + 1,
                            percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                        };

            query = query.Take(6);

            //var query = (from p in _context.Products
            //             join u in _context.Users on p.UserId equals u.UserId
            //             select new
            //             {
            //                 ProductId = p.ProductId,
            //                 UserNickname = u.UserNickname,
            //                 ProductTitle = p.ProductTitle,
            //                 CurrentAmount = p.CurrentAmount,
            //                 TargetAmount = p.TargetAmount,
            //                 Startime = p.Startime,
            //                 Endtime = p.Endtime,
            //                 days = (p.Endtime - DateTime.Now).Days + 1,
            //                 ProductContent = p.ProductContent,
            //                 percent = (int)(((float)p.CurrentAmount / (float)p.TargetAmount) * 100)
            //             });

            return await query.ToListAsync();
        }

        [HttpGet("recent")]//近期新增
        public async Task<ActionResult<IEnumerable<dynamic>>> GetrecentList()
        {
            var addprice = from p in _context.Plans
                           join o in _context.Orders on p.PlanId equals o.PlanId
                           //group p by new {p.ProductId} into g
                           where o.OrderStateId != 4 && o.OrderStateId != 5 && o.OrderStateId != 6
                           select new
                           {
                               ProductId = p.ProductId,
                               toll = (p.PlanPrice + o.AddSponsorship)
                           };

            var sumprice = from add in addprice
                           group add by new { add.ProductId } into g
                           //where p.ProductId == 15
                           select new
                           {
                               ProductId = g.Key.ProductId,
                               toll = g.Sum(ap => ap.toll)
                           };

            var countplan = from p in _context.Plans
                            join o in _context.Orders on p.PlanId equals o.PlanId
                            where o.OrderStateId != 4 && o.OrderStateId != 5 && o.OrderStateId != 6
                            group p by new { p.ProductId } into g
                            select new
                            {
                                ProductId = g.Key.ProductId,
                                Coun = g.Count()
                            };

            var combinesum = from p in _context.Products
                             join sp in sumprice on p.ProductId equals sp.ProductId into ps
                             from sp in ps.DefaultIfEmpty()
                             select new
                             {
                                 ProductId = p.ProductId,
                                 nowmoney = sp.toll == null ? 0 : sp.toll
                             };

            var combine = from o in combinesum
                          join c in countplan on o.ProductId equals c.ProductId into ps
                          from c in ps.DefaultIfEmpty()
                          select new
                          {
                              ProductId = o.ProductId,
                              nowmoney = o.nowmoney,
                              nowperson = c.Coun == null ? 0 : c.Coun
                          };

            var query = from o in combine
                        join product in _context.Products on o.ProductId equals product.ProductId
                        join user in _context.Users on product.UserId equals user.UserId
                        where product.ProductStateId == 3 && product.Endtime > DateTime.Now
                        orderby product.Startime descending
                        select new
                        {
                            ProductId = o.ProductId,
                            UserName = user.UserName,
                            ProductTitle = product.ProductTitle,
                            coverphoto = product.Coverphoto,
                            CurrentAmount = o.nowmoney,
                            TargetAmount = product.TargetAmount,
                            Startime = product.Startime,
                            Endtime = product.Endtime,
                            nowperson = o.nowperson,
                            days = (product.Endtime - DateTime.Now).Days + 1,
                            percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                        };

            query = query.Take(3);

            //var query = (from p in _context.Products
            //             join u in _context.Users on p.UserId equals u.UserId
            //             select new
            //             {
            //                 ProductId = p.ProductId,
            //                 UserNickname = u.UserNickname,
            //                 ProductTitle = p.ProductTitle,
            //                 CurrentAmount = p.CurrentAmount,
            //                 TargetAmount = p.TargetAmount,
            //                 Startime = p.Startime,
            //                 Endtime = p.Endtime,
            //                 days = (p.Endtime - DateTime.Now).Days + 1,
            //                 ProductContent = p.ProductContent,
            //                 percent = (int)(((float)p.CurrentAmount / (float)p.TargetAmount) * 100)
            //             });

            return await query.ToListAsync();
        }


        //[HttpGet("filter/{SelectValue}")]//篩選
        //public async Task<ActionResult<IEnumerable<dynamic>>> GetSelectList(long SelectValue)
        //{
        //    var addprice = from p in _context.Plans
        //                   join o in _context.Orders on p.PlanId equals o.PlanId
        //                   //group p by new {p.ProductId} into g
        //                   //where p.ProductId == 15
        //                   select new
        //                   {
        //                       ProductId = p.ProductId,
        //                       toll = (p.PlanPrice + o.AddSponsorship)
        //                   };

        //    var sumprice = from add in addprice
        //                   group add by new { add.ProductId } into g
        //                   //where p.ProductId == 15
        //                   select new
        //                   {
        //                       ProductId = g.Key.ProductId,
        //                       toll = g.Sum(ap => ap.toll)
        //                   };

        //    var countplan = from p in _context.Plans
        //                    join o in _context.Orders on p.PlanId equals o.PlanId
        //                    group p by new { p.ProductId } into g
        //                    select new
        //                    {
        //                        ProductId = g.Key.ProductId,
        //                        Coun = g.Count()
        //                    };

        //    var combinesum = from p in _context.Products
        //                     join sp in sumprice on p.ProductId equals sp.ProductId into ps
        //                     from sp in ps.DefaultIfEmpty()
        //                     select new
        //                     {
        //                         ProductId = p.ProductId,
        //                         nowmoney = sp.toll == null ? 0 : sp.toll
        //                     };

        //    var combine = from o in combinesum
        //                  join c in countplan on o.ProductId equals c.ProductId into ps
        //                  from c in ps.DefaultIfEmpty()
        //                  select new
        //                  {
        //                      ProductId = o.ProductId,
        //                      nowmoney = o.nowmoney,
        //                      nowperson = c.Coun == null ? 0 : c.Coun
        //                  };

        //    if (SelectValue == 1)
        //    {
        //        var query = from o in combine
        //                    join product in _context.Products on o.ProductId equals product.ProductId
        //                    join user in _context.Users on product.UserId equals user.UserId
        //                    where product.ProductStateId == 3
        //                    orderby (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100) descending
        //                    select new
        //                    {
        //                        ProductId = o.ProductId,
        //                        ProductTitle = product.ProductTitle,
        //                        CurrentAmount = o.nowmoney,
        //                        TargetAmount = product.TargetAmount,
        //                        Startime = product.Startime,
        //                        Endtime = product.Endtime,
        //                        nowperson = o.nowperson,
        //                        days = (product.Endtime - DateTime.Now).Days + 1,
        //                        percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
        //                    };
        //        return await query.ToListAsync();
        //    }
        //    else if (SelectValue == 2)
        //    {
        //        var query = from o in combine
        //                    join product in _context.Products on o.ProductId equals product.ProductId
        //                    join user in _context.Users on product.UserId equals user.UserId
        //                    where product.ProductStateId == 3
        //                    orderby product.Startime
        //                    select new
        //                    {
        //                        ProductId = o.ProductId,
        //                        ProductTitle = product.ProductTitle,
        //                        CurrentAmount = o.nowmoney,
        //                        TargetAmount = product.TargetAmount,
        //                        Startime = product.Startime,
        //                        Endtime = product.Endtime,
        //                        nowperson = o.nowperson,
        //                        days = (product.Endtime - DateTime.Now).Days + 1,
        //                        percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
        //                    };
        //        return await query.ToListAsync();
        //    }
        //    else if (SelectValue == 3)
        //    {
        //        var query = from o in combine
        //                    join product in _context.Products on o.ProductId equals product.ProductId
        //                    join user in _context.Users on product.UserId equals user.UserId
        //                    where product.ProductStateId == 3
        //                    orderby o.nowmoney descending
        //                    select new
        //                    {
        //                        ProductId = o.ProductId,
        //                        ProductTitle = product.ProductTitle,
        //                        CurrentAmount = o.nowmoney,
        //                        TargetAmount = product.TargetAmount,
        //                        Startime = product.Startime,
        //                        Endtime = product.Endtime,
        //                        nowperson = o.nowperson,
        //                        days = (product.Endtime - DateTime.Now).Days + 1,
        //                        percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
        //                    };
        //        return await query.ToListAsync();
        //    }
        //    else if (SelectValue == 4)
        //    {
        //        var query = from o in combine
        //                    join product in _context.Products on o.ProductId equals product.ProductId
        //                    join user in _context.Users on product.UserId equals user.UserId
        //                    where product.ProductStateId == 3
        //                    orderby o.nowperson descending
        //                    select new
        //                    {
        //                        ProductId = o.ProductId,
        //                        ProductTitle = product.ProductTitle,
        //                        CurrentAmount = o.nowmoney,
        //                        TargetAmount = product.TargetAmount,
        //                        Startime = product.Startime,
        //                        Endtime = product.Endtime,
        //                        nowperson = o.nowperson,
        //                        days = (product.Endtime - DateTime.Now).Days + 1,
        //                        percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
        //                    };
        //        return await query.ToListAsync();
        //    }
        //    else
        //    {
        //        return await _context.Products.ToListAsync(); ;
        //    }
        //}

        [HttpGet("filterans/{Selectans}")]//搜尋
        public async Task<ActionResult<IEnumerable<dynamic>>> GetSelecttList(string Selectans)
        {
            var addprice = from p in _context.Plans
                           join o in _context.Orders on p.PlanId equals o.PlanId
                           //group p by new {p.ProductId} into g
                           where o.OrderStateId != 4 && o.OrderStateId != 5 && o.OrderStateId != 6
                           select new
                           {
                               ProductId = p.ProductId,
                               toll = (p.PlanPrice + o.AddSponsorship)
                           };

            var sumprice = from add in addprice
                           group add by new { add.ProductId } into g
                           //where p.ProductId == 15
                           select new
                           {
                               ProductId = g.Key.ProductId,
                               toll = g.Sum(ap => ap.toll)
                           };

            var countplan = from p in _context.Plans
                            join o in _context.Orders on p.PlanId equals o.PlanId
                            where o.OrderStateId != 4 && o.OrderStateId != 5 && o.OrderStateId != 6
                            group p by new { p.ProductId } into g
                            select new
                            {
                                ProductId = g.Key.ProductId,
                                Coun = g.Count()
                            };

            var combinesum = from p in _context.Products
                             join sp in sumprice on p.ProductId equals sp.ProductId into ps
                             from sp in ps.DefaultIfEmpty()
                             select new
                             {
                                 ProductId = p.ProductId,
                                 nowmoney = sp.toll == null ? 0 : sp.toll
                             };

            var combine = from o in combinesum
                          join c in countplan on o.ProductId equals c.ProductId into ps
                          from c in ps.DefaultIfEmpty()
                          select new
                          {
                              ProductId = o.ProductId,
                              nowmoney = o.nowmoney,
                              nowperson = c.Coun == null ? 0 : c.Coun
                          };

            var query = from o in combine
                        join product in _context.Products on o.ProductId equals product.ProductId
                        join user in _context.Users on product.UserId equals user.UserId
                        where product.ProductTitle.Contains(Selectans) && product.ProductStateId == 3 && product.Endtime > DateTime.Now
                        select new
                        {
                            ProductId = o.ProductId,
                            ProductTitle = product.ProductTitle,
                            coverphoto = product.Coverphoto,
                            CurrentAmount = o.nowmoney,
                            TargetAmount = product.TargetAmount,
                            Startime = product.Startime,
                            Endtime = product.Endtime,
                            nowperson = o.nowperson,
                            days = (product.Endtime - DateTime.Now).Days + 1,
                            percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                        };

            //var query = from o in _context.Products
            //            where o.ProductTitle.Contains(Selectans) /*&& product.ProductStateId == 3 && product.Endtime > DateTime.Now*/
            //            select new
            //            {
            //                ProductId = o.ProductId,
            //                ProductTitle = o.ProductTitle,
            //                coverphoto = o.Coverphoto,
            //            };

            //var query = from p in _context.Products
            //            join u in _context.Users on p.UserId equals u.UserId
            //            where p.ProductTitle.Contains(Selectans)
            //            select new
            //            {
            //                ProductId = p.ProductId,
            //                UserNickname = u.UserNickname,
            //                ProductTitle = p.ProductTitle,
            //                CurrentAmount = p.CurrentAmount,
            //                TargetAmount = p.TargetAmount,
            //                Startime = p.Startime,
            //                Endtime = p.Endtime,
            //                days = (p.Endtime - DateTime.Now).Days + 1,
            //                ProductContent = p.ProductContent,
            //                percent = (int)(((float)p.CurrentAmount / (float)p.TargetAmount) * 100)
            //            };
            return await query.ToListAsync();
        }

        [HttpGet("prduct/{productId}")]//哪個產品
        public async Task<ActionResult<IEnumerable<dynamic>>> GetSelecttList(long productId)
        {
            var addprice = from p in _context.Plans
                           join o in _context.Orders on p.PlanId equals o.PlanId
                           //group p by new {p.ProductId} into g
                           where o.OrderStateId != 4 && o.OrderStateId != 5 && o.OrderStateId != 6
                           select new
                           {
                               ProductId = p.ProductId,
                               toll = (p.PlanPrice + o.AddSponsorship)
                           };

            var sumprice = from add in addprice
                           group add by new { add.ProductId } into g
                           //where p.ProductId == 15
                           select new
                           {
                               ProductId = g.Key.ProductId,
                               toll = g.Sum(ap => ap.toll)
                           };

            var countplan = from p in _context.Plans
                            join o in _context.Orders on p.PlanId equals o.PlanId
                            where o.OrderStateId != 4 && o.OrderStateId != 5 && o.OrderStateId != 6
                            group p by new { p.ProductId } into g
                            select new
                            {
                                ProductId = g.Key.ProductId,
                                Coun = g.Count()
                            };

            var combinesum = from p in _context.Products
                             join sp in sumprice on p.ProductId equals sp.ProductId into ps
                             from sp in ps.DefaultIfEmpty()
                             select new
                             {
                                 ProductId = p.ProductId,
                                 nowmoney = sp.toll == null ? 0 : sp.toll
                             };

            var combine = from o in combinesum
                          join c in countplan on o.ProductId equals c.ProductId into ps
                          from c in ps.DefaultIfEmpty()
                          select new
                          {
                              ProductId = o.ProductId,
                              nowmoney = o.nowmoney,
                              nowperson = c.Coun == null ? 0 : c.Coun
                          };

            var query = from o in combine
                        join product in _context.Products on o.ProductId equals product.ProductId
                        join user in _context.Users on product.UserId equals user.UserId
                        where o.ProductId == productId
                        select new
                        {
                            ProductId = o.ProductId,
                            productuserid = user.UserId,
                            username = user.UserName,
                            producttitle = product.ProductTitle,
                            currentamount = o.nowmoney,
                            targetamount = product.TargetAmount,
                            startime = product.Startime.ToString("yyyy/MM/dd"),
                            endtime = product.Endtime.ToString("yyyy/MM/dd"),
                            nowperson = o.nowperson,
                            days = (product.Endtime - DateTime.Now).Days + 1,
                            percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100),
                            vedio = product.ProductVedio,
                            productcontent = product.ProductContent,
                            coverphoto = product.Coverphoto
                        };

            //var query = from p in _context.Products
            //            join u in _context.Users on p.UserId equals u.UserId
            //            where p.ProductId == productId
            //            select new
            //            {
            //                ProductId = p.ProductId,
            //                ProductTitle = p.ProductTitle,
            //                CurrentAmount = p.CurrentAmount,
            //                TargetAmount = p.TargetAmount,
            //                Startime = p.Startime,
            //                Endtime = p.Endtime,
            //                days = (p.Endtime - DateTime.Now).Days + 1,
            //                ProductContent = p.ProductContent,
            //                percent = (int)(((float)p.CurrentAmount / (float)p.TargetAmount) * 100)
            //            };
            return await query.ToListAsync();
        }

        [HttpGet("prductplan/{productId}")]//哪個產品頁方案
        public async Task<ActionResult<IEnumerable<dynamic>>> GetSelectproductplanList(long productId)
        {
            var query = from p in _context.Plans
                        where p.ProductId == productId
                        select new
                        {
                            planid = p.PlanId,
                            planphoto = p.PlanPhoto,
                            planprice = p.PlanPrice,
                            plantitle = p.PlanTitle,
                            plancontent = p.PlanContent
                        };
            return await query.ToListAsync();
        }

        [HttpGet("plan/{productID}")]//專案List
        public async Task<ActionResult<IEnumerable<dynamic>>> GetSelectPlanList(long productId)
        {
            var query = from p in _context.Products
                        join plan in _context.Plans on p.ProductId equals plan.ProductId
                        where p.ProductId == plan.ProductId && plan.ProductId == productId
                        select new
                        {
                            PlanId = plan.PlanId,
                            planphoto = plan.PlanPhoto,
                            ProjectTitle = plan.PlanTitle
                        };
            return await query.ToListAsync();
        }

        [HttpGet("fqa/{fqaproductid}")]//FQA
        public async Task<ActionResult<IEnumerable<dynamic>>> GetSelectorfqaList(long fqaproductid)
        {
            var query = from q in _context.Questions
                        where q.ProductId == fqaproductid
                        select new
                        {
                            questionid = q.QuestionId,
                            questiontitle = q.QuestionTitle,
                            questioncontent = q.QuestionContent
                        };
            return await query.ToListAsync();
        }

        [HttpGet("orderpay/{planID}")]//付款資料
        public async Task<ActionResult<IEnumerable<dynamic>>> GetSelectorderpayList(long planID)
        {
            var query = from plan in _context.Plans
                        where plan.PlanId == planID
                        select new
                        {
                            PlanId = plan.PlanId,
                            planphoto = plan.PlanPhoto,
                            ProjectTitle = plan.PlanTitle,
                            PlanPrice = plan.PlanPrice,
                            plancontent = plan.PlanContent
                        };
            return await query.ToListAsync();
        }

        [HttpGet("order")]//order
        public async Task<ActionResult<IEnumerable<dynamic>>> GetorderList()
        {
            var query = from orders in _context.Orders
                        select orders;
            return await query.ToListAsync();
        }

        [HttpGet("tolpage")]//總頁數
        public IActionResult GettolpageList()
        {
            //var query = _context.Products.Where(x => x.ProductStateId ==3 && x.Endtime > DateTime.Now).Count();

            var addprice = from p in _context.Plans
                           join o in _context.Orders on p.PlanId equals o.PlanId
                           //group p by new {p.ProductId} into g
                           where o.OrderStateId != 5 && o.OrderStateId != 4
                           select new
                           {
                               ProductId = p.ProductId,
                               toll = (p.PlanPrice + o.AddSponsorship)
                           };

            var sumprice = from add in addprice
                           group add by new { add.ProductId } into g
                           //where p.ProductId == 15
                           select new
                           {
                               ProductId = g.Key.ProductId,
                               toll = g.Sum(ap => ap.toll)
                           };

            var countplan = from p in _context.Plans
                            join o in _context.Orders on p.PlanId equals o.PlanId
                            group p by new { p.ProductId } into g
                            select new
                            {
                                ProductId = g.Key.ProductId,
                                Coun = g.Count()
                            };

            var combinesum = from p in _context.Products
                             join sp in sumprice on p.ProductId equals sp.ProductId into ps
                             from sp in ps.DefaultIfEmpty()
                             select new
                             {
                                 ProductId = p.ProductId,
                                 nowmoney = sp.toll == null ? 0 : sp.toll
                             };

            var combine = from o in combinesum
                          join c in countplan on o.ProductId equals c.ProductId into ps
                          from c in ps.DefaultIfEmpty()
                          select new
                          {
                              ProductId = o.ProductId,
                              nowmoney = o.nowmoney,
                              nowperson = c.Coun == null ? 0 : c.Coun
                          };

            var queryy = from o in combine
                         join product in _context.Products on o.ProductId equals product.ProductId
                         join user in _context.Users on product.UserId equals user.UserId
                         where product.ProductStateId == 3
                         orderby o.nowmoney descending
                         select new
                         {
                             ProductId = o.ProductId,
                             UserName = user.UserName,
                             ProductTitle = product.ProductTitle,
                             coverphoto = product.Coverphoto,
                             CurrentAmount = o.nowmoney,
                             TargetAmount = product.TargetAmount,
                             Startime = product.Startime,
                             Endtime = product.Endtime,
                             nowperson = o.nowperson,
                             days = (product.Endtime - DateTime.Now).Days + 1,
                             percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                         };

            var query = (from o in combine
                         join product in _context.Products on o.ProductId equals product.ProductId
                         join user in _context.Users on product.UserId equals user.UserId
                         where product.ProductStateId == 3 && product.Endtime > DateTime.Now
                         //orderby o.nowperson descending
                         select o).Count();

            var tolpage = 0;
            if (query % 6 == 0)
            {
                if (query <= 6)
                {
                    tolpage = 1;
                    return Content(tolpage.ToString());
                }
                else
                {
                    tolpage = (query / 6) ;
                    return Content(tolpage.ToString());
                }
            }
            else
            {
                tolpage = (query / 6) + 1;
                return Content(tolpage.ToString());
            }
        }

        [HttpGet("successpage")]//總頁數
        public IActionResult GetsuccesspageList()
        {
            var addprice = from p in _context.Plans
                           join o in _context.Orders on p.PlanId equals o.PlanId
                           //group p by new {p.ProductId} into g
                           where o.OrderStateId != 5 && o.OrderStateId != 4
                           select new
                           {
                               ProductId = p.ProductId,
                               toll = (p.PlanPrice + o.AddSponsorship)
                           };

            var sumprice = from add in addprice
                           group add by new { add.ProductId } into g
                           //where p.ProductId == 15
                           select new
                           {
                               ProductId = g.Key.ProductId,
                               toll = g.Sum(ap => ap.toll)
                           };

            var countplan = from p in _context.Plans
                            join o in _context.Orders on p.PlanId equals o.PlanId
                            group p by new { p.ProductId } into g
                            select new
                            {
                                ProductId = g.Key.ProductId,
                                Coun = g.Count()
                            };

            var combinesum = from p in _context.Products
                             join sp in sumprice on p.ProductId equals sp.ProductId into ps
                             from sp in ps.DefaultIfEmpty()
                             select new
                             {
                                 ProductId = p.ProductId,
                                 nowmoney = sp.toll == null ? 0 : sp.toll
                             };

            var combine = from o in combinesum
                          join c in countplan on o.ProductId equals c.ProductId into ps
                          from c in ps.DefaultIfEmpty()
                          select new
                          {
                              ProductId = o.ProductId,
                              nowmoney = o.nowmoney,
                              nowperson = c.Coun == null ? 0 : c.Coun
                          };

            var queryy = from o in combine
                         join product in _context.Products on o.ProductId equals product.ProductId
                         join user in _context.Users on product.UserId equals user.UserId
                         where product.ProductStateId == 3
                         orderby o.nowmoney descending
                         select new
                         {
                             ProductId = o.ProductId,
                             UserName = user.UserName,
                             ProductTitle = product.ProductTitle,
                             coverphoto = product.Coverphoto,
                             CurrentAmount = o.nowmoney,
                             TargetAmount = product.TargetAmount,
                             Startime = product.Startime,
                             Endtime = product.Endtime,
                             nowperson = o.nowperson,
                             days = (product.Endtime - DateTime.Now).Days + 1,
                             percent = (int)(((float)o.nowmoney / (float)product.TargetAmount) * 100)
                         };

            var query = (from o in combine
                        join product in _context.Products on o.ProductId equals product.ProductId
                        join user in _context.Users on product.UserId equals user.UserId
                        where product.ProductStateId == 3 && product.Endtime < DateTime.Now && o.nowmoney > product.TargetAmount
                        orderby o.nowperson descending
                        select o).Count();

            //var query = _context.Products.Where(x => x.ProductStateId == 3 && x.Endtime < DateTime.Now).Count();
            var tolpage = 0;
            if (query % 6 == 0)
            {
                if (query <= 6)
                {
                    tolpage = 1;
                    return Content(tolpage.ToString());
                }
                else
                {
                    tolpage = (query / 6);
                    return Content(tolpage.ToString());
                }
            }
            else
            {
                tolpage = (query / 6) + 1;
                return Content(tolpage.ToString());
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<Order>> PostOrder(Order order)
        //{
        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Order>> PostOrder(Order order)
        //{
        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        //}
    }
}
