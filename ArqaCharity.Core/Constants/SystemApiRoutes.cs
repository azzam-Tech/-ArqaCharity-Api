namespace ArqaCharity.Core.Constants
{
    public static class SystemApiRoutes
    {
        public static class Auth
        {
            public const string Base = "api/Auth";
            public const string Login = Base + "/login";
        }

        public static class Emergency
        {
            public const string Base = "emergency";
            public const string Check = Base + "/check";
            public const string Create = Base + "/create";
        }

        public static class Admins
        {
            public const string Base = "api/Admins";
            public const string Create = Base; // POST
        }

        public static class Needy
        {
            public const string Base = "api/Needy";
            public const string GetAll = Base;
            public const string GetById = Base + "/{id}";
            public const string Add = Base;
        }

        public static class DonationAppointments
        {
            public const string Base = "api/DonationAppointments";
            public const string GetAllPending = Base + "/pending";
            public const string Create = Base;
            public const string Approve = Base + "/{id}/approve";
            public const string Reject = Base + "/{id}/reject";
        }

        public static class Volunteers
        {
            public const string Base = "api/Volunteers";
            public const string GetAll = Base;
            public const string Add = Base;
        }

        public static class Memberships
        {
            public const string Base = "api/Memberships";
            public const string GetAll = Base;
            public const string Register = Base;
        }

        public static class Projects
        {
            public const string Base = "api/Projects";
            public const string GetAll = Base;
            public const string Create = Base;
        }

        public static class FinancialReports
        {
            public const string Base = "api/FinancialReports";
            public const string GetAll = Base;
            public const string Add = Base;
        }

        public static class News
        {
            public const string Base = "api/News";
            public const string GetAll = Base;
            public const string Create = Base;
        }

        public static class Donations
        {
            public const string Base = "api/Donations";
            public const string GetTotal = Base + "/total";
            public const string GetByProject = Base + "/project/{projectId}";
            public const string Record = Base;
        }
    }
}
