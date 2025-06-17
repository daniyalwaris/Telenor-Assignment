# Broadband Product Availability Test Automation

This purpose of this is to automates the broadband product availability flow on [Telenor.se], Where simulating a user's experience from landing on the homepage to selecting a broadband product based on address and apartment selection.

## ✅ Purpose
This automation test is built for a job assignment and demonstrates:
- Modular and reusable page object design.
- Handling of dynamic and frequently changing UI.
- Element interactions (keyboard-based navigation, dropdown selections, etc.)



## 🧪 Test Scenario
The test performs the following steps:
1. **Navigate to** `https://www.telenor.se/`
2. **Accept cookies**
3. Open the **"Handla"** (or A/B variant: "Produkter och tjänster") menu
4. Click the **"Bredband"** option
5. Search for a given **address** (e.g. `"Storgatan 1, Stockholm"`)
6. Select a **random apartment** from the dropdown list
7. Wait for and verify that **"Bredband via 5G"** or similar product appears in the product grid

---

## 🧱 Project Structure
```
TelenorTestAutomation/
├── Pages/
│   ├── HomePage.cs            // Handles cookie, menu, and broadband navigation
│   └── BroadbandPage.cs       // Handles address entry, apartment selection, and product grid
├── Tests/
│   └── BroadbandOrderTest.cs  // Main NUnit test class
├── Utilities/
│   └── DriverManager.cs       // Manages singleton WebDriver instance
└── README.md                  // Project documentation
```



## 🚀 How to Run
dotnet test


## 🧠 Design Considerations
- Reusable page objects: Designed to support future tests without rewriting logic.
- Robust selectors: Uses multiple elements locators that can handle layout changes.
- Keyboard interaction: Dropdowns and listboxes use Keys.ArrowDown and Keys.Enter where needed.
- Handles layout shifts like "Handla" vs "Produkter och tjänster".


## 💡 Future Improvements
- Add support for parallel execution.
- Cross-browser testing
- Reporting - Allure or Extended Reporting
