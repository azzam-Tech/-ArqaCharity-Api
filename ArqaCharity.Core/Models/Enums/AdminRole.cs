namespace ArqaCharity.Core.Models.Enums
{
    public enum AdminRole
    {
        ProjectsAndReports = 1,    // ينشر مشاريع وتقارير
        Beneficiaries = 2,         // يدير المحتاجين والمتبرعين
        Volunteers = 3,            // يدير المتطوعين
        Memberships = 4,           // يدير العضويات
        SuperAdmin = 99            // صلاحيات كاملة
    }
}
