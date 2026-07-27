Feature: GlobalSearch

Scenario Outline: Search the website
	Given I am on the EPAM home page
	When I search for "<SearchText>"
	Then every search result should contain "<SearchText>"
	And the search results should not be empty
	Examples: 
	| SearchText |
	| BLOCKCHAIN |
	| Open Source |
	| DevOps |

