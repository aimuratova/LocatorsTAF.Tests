# LocatorsTAF.Tests

Automated UI tests implemented with focus on reliability, maintainability and a strict layered architecture.

## Project Improvements (compared to the previous project)

- Optimized tests  
  Two tests, `CareerSearchTests` and `CarouselArticleTest`, were stabilized and optimized for reliability and speed by improving wait strategies, reducing duplication and clarifying assertions.

- Page Object Model (POM)  
  Tests were migrated to use Page Objects (classes under `LocatorsTAF.BusinessLayer/Pages`) to decouple test logic from UI details and simplify maintenance.

- Logging and Screenshot Services  
  A centralized logging service (`LocatorsTAF.CoreLayer.Utilities.LoggerService`) with `log4net` is used for structured runtime logs. A screenshot service (`LocatorsTAF.CoreLayer.Utilities.ScreenshotMakerService`) captures failure screenshots automatically during teardown.

- Layered Architecture  
  The codebase follows a layered structure to enforce separation of concerns and make the test framework extensible and maintainable.

## Layered project structure (overview)

- Tests layer (`LocatorsTAF.Tests/Tests`)  
  Contains test classes and the base test lifecycle logic (`BaseTest.cs`). Tests use Page Objects from the Business layer and services from Core layer.

- Business layer – Pages (`LocatorsTAF.BusinessLayer/Pages`)  
  Implements Page Objects (e.g. `MainPage`, `JobsPage`, `InsightsPage`, `BasePage`). All page-specific locators and interactions live here.

- Core layer (`LocatorsTAF.CoreLayer`)  
  - `Driver` — browser and driver management (`DriverManager`, `WebdriverWrapper`)  
  - `Element` — wrappers for `IWebElement` and custom element helpers (`WebElementWrapper`)  
  - `Interfaces` — abstract service contracts (`ILoggingService`, `IScreenshotMakerService`, `IWebDriverWrapper`, etc.)  
  - `Utilities` — concrete implementations (e.g. `LoggerService`, `ScreenshotMakerService`) and low-level helpers

- Configuration & infra  
  Test-wide logging is configured in `TestSetup.cs` using `log4net.config`. Screenshots are written to `Screenshots` under the test execution folder (see `ScreenshotMakerService`).

This layering enforces: Tests -> Pages -> Core Services/Utilities -> Driver/Elements.

## Tests (file-by-file description)

- `BaseTest.cs`  
  Central test fixture base. Initializes `DriverManager` and `WebdriverWrapper`, configures `LoggerService` and `ScreenshotMakerService` in `SetUp`. On `TearDown`, takes screenshot on test failure and quits the browser. Provides shared `Logger` and `DriverWrapper` to tests.

- `GlobalSearchTests.cs`  
  Parameterized search verification. Uses `MainPage` to perform a global search for terms (`"BLOCKCHAIN"`, `"Cloud"`, `"Automation"`) and asserts that returned result links are not empty and contain the search text. Uses `MainPage.PerformGlobalSearch(...)` and `SearchResultsPage.GetResultLinks()`.

- `CareerSearchTests.cs`  
  End-to-end flow that navigates to careers -> jobs, filters by `country`, enters `jobTitle`, toggles remote option, searches and verifies that the selected job result description contains the searched job title. Uses `CareersPage`, `JobsPage` and includes explicit waits for loaders.

- `CarouselArticleTest.cs`  
  Verifies carousel navigation on the insights page. Navigates to `InsightsPage`, advances the carousel, captures the article title, opens the article and asserts the opened article title matches the preview. Contains explicit waits to stabilize carousel interactions.

- `PdfDownloadTest.cs`  
  Clicks a PDF download link via `MainPage.ClickToDownloadFile()` and waits for the file to appear in a temporary `Downloads` folder. Uses a WebDriver-aware wait loop to assert the file is fully downloaded (ensures no `.crdownload` partial file remains).

## Logging and screenshots

- Logging: configured via `log4net` in `TestSetup.cs`. See `log4net.config` for appenders and output paths.
- Screenshots: produced by `ScreenshotMakerService` and saved to the `Screenshots` directory under test run output (configured in `ScreenshotMakerService` to `AppContext.BaseDirectory/Screenshots`). Screenshots are captured automatically when a test fails.

## Prerequisites

- .NET 10 SDK
- Browser driver (e.g. ChromeDriver) compatible with the chosen browser version
- `log4net.config` present at the test run root for logging configuration

