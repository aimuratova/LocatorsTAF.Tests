# LocatorsTAF.Tests

Short description: Automated UI tests implemented for the project with a focus on reliability, maintainability and clear separation of concerns.

## Project Improvements (compared to previous project)

- Optimized tests  
  Two previously flaky/slow tests have been optimized for improved stability and execution speed. Changes include improved wait strategies, reduced duplication, and clearer assertions.

- Page Object Model (POM)  
  Test code has been migrated to the Page Object pattern to decouple test logic from UI details and to make maintenance and extension straightforward.

- Logging and Screenshot Services  
  A centralized logging service captures structured runtime information. A screenshot service records failure screenshots to accelerate debugging and test reporting.

- Layered Architecture  
  The project follows a strict layered architecture: Tests -> Pages -> Services -> Utilities. This separation of concerns improves readability, testability and long‑term maintainability.

## Prerequisites

- .NET 10 SDK
- Browser drivers as required by the chosen WebDriver implementation

