namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Agency : IAgency
    {

        private Dictionary<string, Invoice> invoices;

        public Agency()
        {
            invoices = new Dictionary<string, Invoice>();
        }

        public void Create(Invoice invoice)
        {
            if (this.Contains(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }

            invoices.Add(invoice.SerialNumber, invoice);
        }

        public void ThrowInvoice(string number)
        {
            if (!this.Contains(number))
            {
                throw new ArgumentException();
            }

            invoices.Remove(number);
        }

        public void ThrowPayed()
        {
            invoices = invoices
                .Where(kvp => kvp.Value.Subtotal > 0)
                .ToDictionary(i => i.Key, i => i.Value);
        }

        public int Count()
        {
            return invoices.Count;
        }

        public bool Contains(string number)
        {
            return invoices.ContainsKey(number);
        }

        public void PayInvoice(DateTime due)
        {
            var list = invoices.Values
                .Where(i => i.DueDate.Equals(due));
            if (list.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var invoice in list)
            {
                invoice.Subtotal = 0;
            }
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            return invoices.Values
                .Where(i => i.IssueDate >= start && i.IssueDate <= end)
                .OrderBy(i => i.IssueDate)
                .ThenBy(i => i.DueDate);
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            var list = invoices.Values
                .Where(i => i.SerialNumber.Contains(serialNumber))
                .OrderByDescending(i => i.SerialNumber);
            if (list.Count() == 0)
            {
                throw new ArgumentException();
            }

            return list;
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var list = invoices.Values
                .Where(i => i.DueDate > start && i.DueDate < end);
            if (list.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var invoice in list)
            {
                invoices.Remove(invoice.SerialNumber);
            }

            return list;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            return invoices.Values
                .Where(i => (int)i.Department == (int)department)
                .OrderByDescending(i => i.Subtotal)
                .ThenBy(i => i.IssueDate);
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {
            return invoices.Values
                .Where(i => i.CompanyName == company)
                .OrderByDescending(i => i.SerialNumber);
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            var list = invoices.Values
                .Where(i => i.DueDate.Equals(dueDate));
            if (list.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var invoice in list)
            {
                invoice.DueDate = invoice.DueDate.AddDays(days);
            }
        }
    }
}
