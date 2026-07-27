Feature: Services navigation

 Scenario Outline: Validate navigation to services page

	Given I open EPAM home page
	When I select "<Service>"
	Then page title should contain "<Service>"
	And Our Related Expertise section is displayed
	Examples: 
	| Service |
    | Generative AI |
    | Responsible AI |

