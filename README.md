# specflow-playwright-C#-framework

# SpecFlow Playwright Framework

## 🚀 Introduction
This repository contains an end-to-end test automation framework built using **SpecFlow, Playwright, and C#**. It is designed to automate web applications using **Behavior-Driven Development (BDD)** with **Gherkin syntax**.

---

## 📁 Folder Structure

```
SpecFlowPlaywrightFramework
│── Dependencies
│── Screenshot  
│   ├── Passed (Stores screenshots of passed test cases)
│── src
│   ├── test
│   │   ├── features (Contains feature files written in Gherkin syntax)
│   │   │   ├── Sample.feature
│   │   ├── pages (Page Object Model for structuring page elements & methods)
│   │   │   ├── SamplePage.cs
│   │   ├── stepdefinitions (Step definition files implementing feature steps)
│   │   │   ├── SampleStepDefinition.cs
│   │   ├── utils (Utility classes for hooks, setup, and common methods)
│   │   │   ├── CommonMethods.cs
│   │   │   ├── Hooks.cs
│   │   │   ├── PageBase.cs
│   │   │   ├── SetupClass.cs
│── .gitignore
│── README.md (You are here 📌)
│── specflow.json (Configuration file for SpecFlow)
```

---

## 🛠️ Tech Stack
- **Language:** C#
- **Automation Framework:** SpecFlow (BDD with Gherkin)
- **UI Testing Library:** Playwright
- **Test Runner:** NUnit
- **Assertions:** NUnit Assertions
- **Dependency Management:** NuGet

---

## 🔧 Prerequisites
Ensure you have the following installed before setting up the project:
- .NET SDK (>= 6.0) - [Download Here](https://dotnet.microsoft.com/en-us/download)
- Visual Studio with C# development tools
- Playwright CLI: Install via command below:
  ```sh
  dotnet tool install --global Microsoft.Playwright.CLI
  ```
- Install Playwright Browsers:
  ```sh
  playwright install
  ```

---

## 📥 Installation & Setup
Clone the repository and install dependencies:

```sh
git clone https://github.com/ratnesh85/specflow-playwright-C_Sharp-framework.git
cd SpecFlowPlaywrightFramework
dotnet restore
```

---

## 🚀 Running Tests
### Run all tests:
```sh
dotnet test
```

### Run tests with tags:
```sh
dotnet test --filter TestCategory=Smoke
```

### Generate test report:
```sh
dotnet test --logger "html;LogFileName=TestReport.html"
```

---

## ✍️ Writing Test Cases
### **Feature File (Sample.feature)**
```gherkin
Feature: Sample Feature
  Scenario: Verify user can search in Google
    Given I navigate to "https://www.google.com"
    When I enter "SpecFlow Playwright" in the search bar
    Then I should see results related to "SpecFlow Playwright"
```

### **Step Definition (SampleStepDefinition.cs)**
```csharp
[Binding]
public class SampleStepDefinition
{
    private readonly SamplePage _samplePage;
    public SampleStepDefinition()
    {
        _samplePage = new SamplePage();
    }
    
    [Given(@"I navigate to "(.*)"")]
    public async Task GivenINavigateTo(string url)
    {
        await _samplePage.NavigateTo(url);
    }
}
```

---

## 🌟 Key Features
✅ **SpecFlow with Playwright for UI Automation**
✅ **Page Object Model (POM) Design Pattern**
✅ **Parallel Test Execution Support**
✅ **Screenshots on Test Failure**
✅ **Test Execution Reports**
✅ **Support for Environment Variables in Hooks**

---

## 📌 Contribution Guide
Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new feature branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m "Added a new feature"`).
4. Push to the branch (`git push origin feature-branch`).
5. Create a Pull Request.

---

## 💡 License
This project is open-source and available under the **MIT License**.

---

## 📞 Contact
For queries or support, reach out to:
📧 Email: shukla.ratnesh85@gmail.com  
🐦 Twitter/X: [@ratnesh_io](https://twitter.com/ratnesh_io)  


