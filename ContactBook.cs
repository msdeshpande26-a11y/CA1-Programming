using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactBookApp
{
    public class ContactBook
    {
        private readonly List<Contact> contacts = new List<Contact>();

        public void AddContact(Contact contact)
        {
            if (contacts.Any(c => c.MobileNumber == contact.MobileNumber))
            {
                Console.WriteLine("A contact with this mobile number already exists.");
                return;
            }

            contacts.Add(contact);
            Console.WriteLine($"Added: {contact.FirstName} {contact.LastName}");
        }

        public void ShowAllContacts()
        {
            if (!contacts.Any())
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            Console.WriteLine("\nAll Contacts:");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"- {contact.FirstName} {contact.LastName} ({contact.MobileNumber})");
            }
        }

        public void ShowContactDetailsByName(string firstName, string lastName)
        {
            var contact = contacts.FirstOrDefault(c =>
                c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (contact != null)
                contact.DisplayContact();
            else
                Console.WriteLine("Contact not found.");
        }

        public void UpdateContactByName(string firstName, string lastName, string newEmail)
        {
            var contact = contacts.FirstOrDefault(c =>
                c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (contact != null)
            {
                contact.Email = newEmail;
                Console.WriteLine($"Updated email for {contact.FirstName} {contact.LastName}.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public void DeleteContact(string mobileNumber)
        {
            var contact = contacts.FirstOrDefault(c => c.MobileNumber == mobileNumber);
            if (contact != null)
            {
                contacts.Remove(contact);
                Console.WriteLine($"Deleted contact {contact.FirstName} {contact.LastName}.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
    }
}
