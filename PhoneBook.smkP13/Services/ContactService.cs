using PhoneBook.smkP13.Controllers;
using PhoneBook.smkP13.Models;
using Spectre.Console;

namespace PhoneBook.smkP13.Services;

class ContactService
{
    internal void AddContact()
    {
        Contact contact = new();
        ContactCategory contactCategory = new();
        contact.FirstName = UserInputs.GetFirstName("Enter First Name (obligatory)");
        if (UserInputs.Validation("Add a Last Name?")) contact.LastName = UserInputs.GetLastName("Enter Last Name");
        contact.PhoneNumber = UserInputs.GetPhoneNumber("Enter a Phone Number");
        if (UserInputs.Validation("Add an Email Address?")) contact.Email = UserInputs.GetEmail("Enter an Email Address");
        if (UserInputs.Validation("Add Categories?"))
        {
            ContactController.AddContact(contact);
            List<Category>? selectedCategories = UserInputs.ChooseCategories(contact);
            if (selectedCategories != null)
            {
                if (!selectedCategories.Any(x => x.Id == 0))
                {
                    if (selectedCategories.Any(x => x.Id == -1))
                    {
                        Category? newCategory = CategoryService.AddNewCategory();
                        if (newCategory != null) selectedCategories.Add(newCategory);
                    }
                    ContactCategoryController.AddForNewContact(contact, selectedCategories);
                }
            }

        }else ContactController.AddContact(contact);
    }

    internal void DeleteContact()
    {
        Contact contact = UserInputs.GetContact();
        if (contact.Id != 0)
        {
            AnsiConsole.WriteLine($"{contact.FirstName} - {contact.LastName}");
            if (UserInputs.Validation("Are you sure you want to [red]delete[/] this contact?")) ContactController.DeleteContact(contact);
        }
    }

    internal void PrintAllContacts()
    {
        List<Contact> contacts = ContactController.GetAllContacts();
        Panel panel;
        foreach (Contact contact in contacts)
        {
            string contactInfos = "";
            contactInfos += $"Phone Number: {contact.PhoneNumber}\nEmail Address: {contact.Email}\n";
            contact.Categories = CategoryController.GetContactCategories(contact.Id);
            if (contact.Categories != null)
            {
                contactInfos += "Categories: ";
                foreach (Category category in contact.Categories) contactInfos += $"{category.Name}   ";
            }
            panel = new(contactInfos);
            panel.Header($"{contact.FirstName} - {contact.LastName}");
            AnsiConsole.Write(panel);
        }
    }

    internal void PrintSingleContact()
    {
        Contact contact = UserInputs.GetContact();
        if (contact.Id != 0)
        {
            string contactInfos = $"Phone Number: {contact.PhoneNumber}\nEmail Address: {contact.Email}\n";
            contact.Categories = CategoryController.GetContactCategories(contact.Id);
            if (contact.Categories != null)
            {
                contactInfos += "Categories: ";
                foreach (Category category in contact.Categories) contactInfos += $"{category.Name}   ";
            }
            Panel panel = new(contactInfos);
            panel.Header($"{contact.FirstName} - {contact.LastName}");
            AnsiConsole.Write(panel);
        }
    }

    internal void UpdateContact()
    {
        Contact contact = UserInputs.GetContact();
        if (contact.Id != 0)
        {
            string option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose an option below:")
                .AddChoices("First Name", "Last Name", "Phone Number", "Email Address", "Categories", "Cancel"));
            switch (option)
            {
                case "First Name":
                    contact.FirstName = UserInputs.GetFirstName("Enter a new First Name:");
                    ContactController.UpdateContact(contact);
                    break;
                case "Last Name":
                    contact.LastName = UserInputs.GetLastName("Enter a new Last Name:");
                    ContactController.UpdateContact(contact);
                    break;
                case "Phone Number":
                    contact.PhoneNumber = UserInputs.GetPhoneNumber("Enter a new Phone Number:");
                    ContactController.UpdateContact(contact);
                    break;
                case "Email Address":
                    contact.Email = UserInputs.GetEmail("Enter a new Email Address:");
                    ContactController.UpdateContact(contact);
                    break;
                case "Categories":
                    List<Category>? selectedCategories = UserInputs.ChooseCategories(contact);
                    if (selectedCategories != null)
                    {
                        if (!selectedCategories.Any(x => x.Id == 0))
                        {
                            if (selectedCategories.Any(x => x.Id == -1))
                            {
                                Category? newCategory = CategoryService.AddNewCategory();
                                if (newCategory != null) selectedCategories.Add(newCategory);
                            }
                            contact.Categories = selectedCategories;
                            ContactController.UpdateContact(contact);
                        }
                    }
                    break;
                default: break;
            }
        }
    }
}
