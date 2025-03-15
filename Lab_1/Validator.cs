using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab_1
{
    public static class Validator
    {
        static bool validate50(int act) => act > 0 && act <= 50; 
        static bool validate20(int act) => act > 0 && act <= 20; 
        static bool validateDuration(int act) => act > 0 && act <= 90;
        static bool validateAmount(int act) => act > 0 && act <= 30;      
        static bool validateDiscount(int act) => act >= 0 && act <= 100;
        static bool validateNotZero(int act) => act > 0;
        static bool validateOnlyLetters50(string str) => validate50(str.Length) && Regex.IsMatch(str, @"^[a-zA-Zа-яА-ЯҐґЄєІіЇї '-]+$");
        static bool validateOnlyLetters20(string str) => validate20(str.Length) && Regex.IsMatch(str, @"^[a-zA-Zа-яА-ЯҐґЄєІіЇї '-]+$");
        static bool validatePhoneMask(string str) => Regex.IsMatch(str, @"^\+\d{11,15}$");
        static bool validateRating(int rate) => rate >= 0 && rate <= 10;
        static bool validateReviewText(int textLength) => textLength > 0 && textLength <= 300;

        public static void ValidateClient(Client client)
        {
            ValidateName(client);
            ValidateSurname(client);
            ValidatePatronymic(client);           
            ValidateAddress(client);
            ValidatePhone(client);
        }
        public static void ValidateRoute(Route route)
        {
            ValidateCountry(route);
            ValidateClimate(route);
            ValidateDuration(route);
            ValidateHotel(route);
            ValidateMoney(route);
        }
        public static void ValidateTravel(Travel travel)
        {
            ValidateRouteId(travel);
            ValidateClientId(travel);           
            ValidateAmount(travel);
            ValidateDiscount(travel);
            ValidateCost(travel);
        }

        static void ValidateName(Client client)
        {
            if (!validateOnlyLetters50(client.Name)) throw new Exception("Invalid client name");           
        }
        static void ValidateSurname(Client client)
        {
            if (!validateOnlyLetters50(client.Surname)) throw new Exception("Invalid client surname");
        }
        static void ValidatePatronymic(Client client)
        {
            if (!validateOnlyLetters50(client.Patronymic)) throw new Exception("Invalid client patronimic");
        }
        static void ValidateAddress(Client client)
        {
            if (!validate50(client.Address.Length)) throw new Exception("Invalid client address");          
        }
        static void ValidatePhone(Client client)
        {
            if (!validatePhoneMask(client.Phone)) throw new Exception("Invalid client phone");
        }

        static void ValidateCountry(Route route)
        {
            if (!validateOnlyLetters20(route.Country)) throw new Exception("Invalid route country");
        }
        static void ValidateClimate(Route route)
        {
            if (!validateOnlyLetters20(route.Climate)) throw new Exception("Invalid route climate");
        }
        static void ValidateHotel(Route route)
        {
            if (!validateOnlyLetters20(route.Hotel)) throw new Exception("Invalid route hotel");
        }
        static void ValidateMoney(Route route)
        {
            if (!validateNotZero(route.Money)) throw new Exception("Invalid route price");
        }
        static void ValidateDuration(Route route)
        {
            if (!validateDuration(route.Duration)) throw new Exception("Invalid route duration");
        }
        
        static void ValidateRouteId(Travel travel)
        {
            if (!validateNotZero(travel.RouteId)) throw new Exception("Invalid travel route id");
        }
        static void ValidateClientId(Travel travel)
        {
            if (!validateNotZero(travel.ClientId)) throw new Exception("Invalid travel client id");
        }
        static void ValidateAmount(Travel travel)
        {
            if (!validateAmount(travel.Amount)) throw new Exception("Invalid travel amount");
        }
        static void ValidateDiscount(Travel travel)
        {
            if (!validateDiscount(travel.Discount)) throw new Exception("Invalid travel discount");
        }
        static void ValidateCost(Travel travel)
        {
            if (!validateNotZero(travel.Cost)) throw new Exception("Invalid travel cost");
        }
    }
}
