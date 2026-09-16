pipeline {
    agent any

    parameters {
        choice(
            name: 'BROWSER',
            choices: ['Chrome', 'Firefox', 'Edge'],
            description: 'Browser to use for UI tests'
        )
    }

    triggers {
        cron('H 2 * * *')
    }

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        BUILD_CONFIGURATION = 'Release'
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration %BUILD_CONFIGURATION% --no-restore'
            }
        }

        stage('API Tests') {
            steps {
                catchError(
                    buildResult: 'SUCCESS',
                    stageResult: 'FAILURE'
                ) {
                    bat '''
                        dotnet test src/GH.TAF.Tests/GH.TAF.Tests.csproj ^
                        --configuration %BUILD_CONFIGURATION% ^
                        --no-build ^
                        --filter "Category=API"
                    '''
                }
            }
        }

        stage('UI Tests') {
            steps {
                bat '''
                    dotnet test src/GH.TAF.Tests/GH.TAF.Tests.csproj ^
                    --configuration %BUILD_CONFIGURATION% ^
                    --no-build ^
                    --filter "Category=UI"
                '''
            }
        }
    }

    post {
        always {
            archiveArtifacts(
                artifacts: '**/TestResults/**/*, **/Screenshots/**/*',
                allowEmptyArchive: true
            )

            junit(
                testResults: '**/TestResults/*.xml',
                allowEmptyResults: true
            )
        }
    }
}