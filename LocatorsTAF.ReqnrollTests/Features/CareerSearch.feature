Feature: CareerSearch

Scenario Outline: Search for remote vacancies

	Given I open EPAM home page
	When I navigate to Careers
    And I start a job search
    And I select "<Country>" as the country
    And I enter "<JobTitle>" as the job title
    And I filter by remote vacancies
    And I submit the search
    Then the last search result should contain "<JobTitle>"
	Examples:
      | Country    | JobTitle |
      | Kazakhstan | Engineer |
      | Poland     | Java     |      
      | India      | Python   |
