using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Cryptography;
using System.Text;
using web.Db;

namespace web.Models;

public class Init
{
    const string REVIEW_ID_01 = "review-Id-01";
    const string REVIEW_ID_02 = "review-Id-02";
    const string REVIEW_ID_03 = "review-Id-03";
    const string REVIEW_ID_04 = "review-Id-04";
    const string REVIEW_ID_05 = "review-Id-05";
    const string REVIEW_ID_06 = "review-Id-06";
    const string REVIEW_ID_07 = "review-Id-07";

    public static async Task WriteDb(MyContext _context, QRPulseConfig _qrpulseConfig)
    {
        // Удаление
        _context.RemoveRange(_context.Answer.IgnoreQueryFilters());
        await _context.SaveChangesAsync();

        _context.RemoveRange(_context.Review.IgnoreQueryFilters());
        await _context.SaveChangesAsync();

        _context.RemoveRange(_context.Bonuce.IgnoreQueryFilters());
        await _context.SaveChangesAsync();

        _context.RemoveRange(_context.Guest.IgnoreQueryFilters());
        await _context.SaveChangesAsync();

        _context.RemoveRange(_context.User.IgnoreQueryFilters());
        await _context.SaveChangesAsync();

        _context.RemoveRange(_context.Company.IgnoreQueryFilters());
        await _context.SaveChangesAsync();

        // Добавление
        var companies = new List<Company>
        {
            new Company
            {
                id = _qrpulseConfig.SysCompanyId,
                fullName = "Восточный квартал",
                shortName = "ВК",
                contactEmail = "info@vostoc.ru",
                contactPhone = "74991123300",
            },
            new Company
            {
                id = "company-2",
                fullName = "Белый журавль",
                shortName = "БЖ",
                contactEmail = "info@belyijuravl.ru",
                contactPhone = "79099092002",
            },
            new Company
            {
                id = "company-3",
                fullName = "Апрель",
                shortName = "Апрель",
                contactEmail = "info@april.ru",
                contactPhone = "74957395765",
            },
            new Company
            {
                id = "company-4",
                fullName = "Нескучный сад",
                shortName = "НС",
                contactEmail = "info@neskuchyisad.ru",
                contactPhone = "74957395765",
            },
            new Company
            {
                id = "company-5",
                fullName = "Парк авеню",
                shortName = "ПА",
                contactEmail = "info@parkavenu.ru",
                contactPhone = "74957395765",
            },
        };
        _context.Company.AddRange(companies);
        await _context.SaveChangesAsync();


        List<User> users = new List<User>();
        string salt = Nanoid.Nanoid.Generate(size: 31);
        string hash = ComputePasswordSHA256(_qrpulseConfig.SysAdminPass, salt);
        if (string.IsNullOrEmpty(hash))
        {
            throw new Exception("Ошибка вычисления хэша");
        }
        User sysUser = new User
        {
            login = _qrpulseConfig.SysAdminLogin,
            salt = salt,
            hash = hash,
            companyId = _qrpulseConfig.SysCompanyId,
            contactEmail = "john.bg@vostoc.ru",
            contactPhone = "79099092003",
            gender = "M",
            name = "John",
            surname = "Maria",
            dateBirth = new DateTime(1998, 1, 3),

        };
        users.Add(sysUser);
        string salt2 = Nanoid.Nanoid.Generate(size: 31);
        string hash2 = ComputePasswordSHA256("pass", salt);
        User user2 = new User
        {
            id = "user-id-02",
            login = "elena.smirnova@vostoc.ru",
            salt = salt2,
            hash = hash2,
            companyId = _qrpulseConfig.SysCompanyId,
            contactEmail = "elena.smirnova@vostoc.ru",
            contactPhone = "79099092002",
            gender = "F",
            name = "Helen",
            surname = "Alexander",
            dateBirth = new DateTime(2002, 3, 5),
        };
        users.Add(user2);                
        _context.AddRange(users);
        await _context.SaveChangesAsync();

        // Тестовые опросы
        List<Review> reviews = new List<Review>()
        {
            new Review
            {
                companyId = _qrpulseConfig.SysCompanyId,
                id = REVIEW_ID_01,
                active = true,
                key = "review01",
                description = $"Опрос NPS в ресторане \"осточный квартал\"",
                question1 = "Как вас зовут?",
                question2 = "Часто ли вы посещаете наш ресторан?",
                question3 = "Что нам необходимо улучшить?",
            },
            new Review
            {
                companyId = _qrpulseConfig.SysCompanyId,
                id = REVIEW_ID_02,
                active = true,
                key = "review02",
                description = "Опрос о качестве обслуживания",
                question1 = "Понравилось ли вам обслуживание в нашем ресторане?",
                question2 = "Оцените качество блюд?",
                question3 = "Оцените доброжелательность официантов",
            },
            new Review
            {
                companyId = _qrpulseConfig.SysCompanyId,
                id = REVIEW_ID_03,
                active = true,
                key = "review03",
                description = "Опрос о доставке",
                question1 = "Пользуетесь ли вы доставкой из нашего ресторана?",
                question2 = "Знаете ли вы, что доставка нашего ресторана, в отличие от агрегаторов, работает круглосуточно?",
                question3 = "Оцените работу нашей службы доставки?",
            },
            new Review
            {
                companyId = "company-2",
                id = REVIEW_ID_04,
                active = true,
                key = "review04",
                description = "Опрос о качестве обслуживания в ресторане \"Белый журавль\"",
                question1 = "Что вам понравилось в обслуживание нашего ресторана?",
                question2 = "Что вам не понравилось в обслуживание нашего ресторана?",
                question3 = "Оцените обслуживание в нашем ресторане",
            },
            new Review
            {
                companyId = "company-3",
                id = REVIEW_ID_05,
                active = true,
                key = "review05",
                description = "Опрос о доставке в ресторане \"Апрель\"",
                question1 = "Оцените работу службы доставки",
                question2 = "Заказ доставили вовремя?",
                question3 = "Оцените качество упаковки",
            },
            new Review
            {
                companyId = "company-4",
                id = REVIEW_ID_06,
                active = true,
                key = "review06",
                description = "Опрос о доставке в ресторане \"Нескучный сад\"",
                question1 = "Оцените работу службы доставки",
                question2 = "Заказ доставили вовремя?",
                question3 = "Оцените качество упаковки",
            },
            new Review
            {
                companyId = "company-5",
                id = REVIEW_ID_07,
                active = true,
                key = "review07",
                description = "Опрос NPS в ресторане \"Парк авеню\"",
                question1 = "Как вас зовут?",
                question2 = "Часто ли вы посещаете наш ресторан?",
                question3 = "Что вы бы предложили улучшить?",
            },
        };               
        _context.AddRange(reviews);
        await _context.SaveChangesAsync();

        List<Answer> answers = new List<Answer>
        {
            new Answer
            {
                id = "answ-id-01",
                date = DateTime.Now,
                mark = 1,
                text1 = "Влада",
                text2 = "Часто",
                text3 = "Ничего",
                phone = "79161111101",

                reviewId = REVIEW_ID_01,                 
                companyId = _qrpulseConfig.SysCompanyId,
            },
            new Answer
            {
                id = "answ-id-02",
                date = DateTime.Now,
                mark = 2,
                text1 = "Да",
                text2 = "Все очень вкусно",
                text3 = "Все были доброжелательны",
                phone = "79161111102",

                reviewId = REVIEW_ID_02,
                companyId = _qrpulseConfig.SysCompanyId,
            },
            new Answer
            {
                id = "answ-id-03",
                date = DateTime.Now,
                mark = 3,
                text1 = "Да",
                text2 = "Да, это очень удобно",
                text3 = "Заказ пришел вовремя",
                phone = "79161111103",

                reviewId = REVIEW_ID_03,
                companyId = _qrpulseConfig.SysCompanyId,
            },
            new Answer
            {
                id = "answ-id-04",
                date = DateTime.Now,
                mark = 4,
                text1 = "Еду приносили быстро, она была вкусной",
                text2 = "Слишкомс дорого",
                text3 = "5",
                phone = "79161111104",

                reviewId = REVIEW_ID_04,
                companyId = _qrpulseConfig.SysCompanyId,
            },
            new Answer
            {
                id = "answ-id-05",
                date = DateTime.Now,
                mark = 5,
                text1 = "Все отлично",
                text2 = "Да",
                text3 = "Хорошая упаковка, еда была теплой",
                phone = "79161111105",

                reviewId = REVIEW_ID_05,
                companyId = _qrpulseConfig.SysCompanyId,
            },

            new Answer
            {
                id = "answ-id-21",
                date = DateTime.Now,
                mark = 1,
                text1 = "Все было хорошо",
                text2 = "Да",
                text3 = "Еда доставили теплой",
                phone = "79161111121",

                reviewId = REVIEW_ID_06,
                companyId = _qrpulseConfig.SysCompanyId,
            },
            new Answer
            {
                id = "answ-id-22",
                date = DateTime.Now,
                mark = 2,
                 text1 = "Саша",
                text2 = "Не очень часто",
                text3 = "Добавить новые акции",
                phone = "79161111122",

                reviewId = REVIEW_ID_07,
                companyId = _qrpulseConfig.SysCompanyId,
            },
            new Answer
            {
                id = "answ-id-23",
                date = DateTime.Now,
                mark = 3,
                text1 = "В целом да",
                text2 = "Интерьер очень понравился и еда была вкусной",
                text3 = "Хорошие официанты",
                phone = "79161111123",

                reviewId = REVIEW_ID_02,
                companyId = _qrpulseConfig.SysCompanyId,
            },
        

        };        
        _context.AddRange(answers);
        await _context.SaveChangesAsync();

        string salt11 = Nanoid.Nanoid.Generate(size: 31);
        string hash11 = ComputePasswordSHA256("pass", salt11);
        string salt12 = Nanoid.Nanoid.Generate(size: 31);
        string hash12 = ComputePasswordSHA256("pass", salt12);
        string salt13 = Nanoid.Nanoid.Generate(size: 31);
        string hash13 = ComputePasswordSHA256("pass", salt13);

        List<Guest> guests = new List<Guest>
        {
            new Guest
            {
                id = "guest-id-01",
                companyId = _qrpulseConfig.SysCompanyId,
                login = "An",
                salt = salt11,
                hash = hash11,
                gkey = "gk1",
                name = "Анна",
                surname = "Витальевна",
                email = "an@mos.ru",
                phone = "79162223311",
                dateBirth = new DateTime(2001, 1, 1),
                gender = "F",
            },
            new Guest
            {
                id = "guest-id-02",
                companyId = _qrpulseConfig.SysCompanyId,
                login = "login2",
                salt = salt12,
                hash = hash12,
                gkey = "gk2",
                name = "Василий",
                surname = "Иванович",
                email = "vasyl2@mos.ru",
                phone = "79162223322",
                dateBirth = new DateTime(2002, 2, 2),
                gender = "M",
            },
            new Guest
            {
                id = "guest-id-03",
                companyId = _qrpulseConfig.SysCompanyId,
                login = "login3",
                salt = salt13,
                hash = hash13,
                gkey = "gk3",
                name = "Мария",
                surname = "Ивановна",
                email = "mary3@mos.ru",
                phone = "79162223333",
                dateBirth = new DateTime(2003, 3, 3),
                gender = "F",
            },
        };
        _context.AddRange(guests);
        await _context.SaveChangesAsync();

        List<Bonuce> bonuces = new List<Bonuce>
        {
            new Bonuce
            {
                id = "bon-g1-id01",
                guestId = "guest-id-01",
                companyId = _qrpulseConfig.SysCompanyId,
                dtd = new DateTime(2023, 5, 4),
                sum = 100,
                remark = "Приветственный бонус за регистрацию",
            },
            new Bonuce
            {
                id = "bon-g-id02",
                guestId = "guest-id-01",
                companyId = _qrpulseConfig.SysCompanyId,
                dtd = new DateTime(2023, 5, 7),
                sum = 54,
                remark = "Оплата заказа",
            },
            new Bonuce
            {
                id = "bon-g1-id03",
                guestId = "guest-id-01",
                companyId = _qrpulseConfig.SysCompanyId,
                dtd = new DateTime(2023, 5, 9),
                sum = 87,
                remark = "Оплата доставки",
            },

            new Bonuce
            {
                id = "bon-g2-id01",
                guestId = "guest-id-02",
                companyId = _qrpulseConfig.SysCompanyId,
                dtd = new DateTime(2023, 5, 9),
                sum = 100,
                remark = "Приветственный бонус за регистрацию",
            },
            new Bonuce
            {
                id = "bon-g2-id02",
                guestId = "guest-id-02",
                companyId = _qrpulseConfig.SysCompanyId,
                dtd = new DateTime(2023, 5, 16),
                sum = 134,
                remark = "Оплата заказа",
            },
            new Bonuce
            {
                id = "bon-g2-id03",
                guestId = "guest-id-02",
                companyId = _qrpulseConfig.SysCompanyId,
                dtd = new DateTime(2023, 6, 9),
                sum = 415,
                remark = "Оплата доставки",
            },

            new Bonuce
            {
                id = "bon-g3-id01",
                guestId = "guest-id-03",
                companyId = _qrpulseConfig.SysCompanyId,
                dtd = new DateTime(2023, 4, 25),
                sum = 100,
                remark = "Приветственный бонус за регистрацию",
            },
            new Bonuce
            {
                id = "bon-g3-id02",
                guestId = "guest-id-03",
                companyId = _qrpulseConfig.SysCompanyId,
                dtd = new DateTime(2023, 4, 28),
                sum = 254,
                remark = "Оплата заказа",
            },
            new Bonuce
            {
                id = "bon-g3-id03",
                guestId = "guest-id-03",
                companyId = _qrpulseConfig.SysCompanyId,
                dtd = new DateTime(2023, 5, 28),
                sum = -354,
                remark = "Неиспользованные бонусы сгорели",
            },
        };
        _context.AddRange(bonuces); 
        await _context.SaveChangesAsync();

    }

    /// <summary>
    /// Вычисление SHA256
    /// </summary>
    /// <param name="data"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    protected static string ComputePasswordSHA256(string data, string salt)
    {
        if (string.IsNullOrEmpty(data)) return "";
        try
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data + salt));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Error("Ошибка вычисления SHA256.", ex.Message);
        }
        return "";
    }
}
