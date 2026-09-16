pipeline {

    agent any

    parameters {
        choice(
            name: 'BROWSER',
            choices: ['Chrome', 'Firefox'],
            description: 'Browser to use for UI tests'
        )
    }

    triggers {
        // Run every weekday at a random minute/hour
        // to avoid all Jenkins jobs starting simultaneously.
        cron('H 6 * * 1-5')
    }

    environment {
        DOTNET_VERSION = '10.0.x'
        TEST_PROJECT = 'src/GH.TAF.Tests/GH.TAF.Tests.csproj'
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Show Project Structure') {
            steps {
                sh '''
                    echo "Current directory:"
                    pwd

                    echo "Project files:"
                    find . -name "*.csproj"
                '''
            }
        }

        stage('Restore') {
            steps {
                sh '''
                    dotnet restore "$TEST_PROJECT"
                '''
            }
        }

        stage('Build') {
            steps {
                sh '''
                    dotnet build "$TEST_PROJECT" \
                        --configuration Release \
                        --no-restore
                '''
            }
        }

        stage('API Tests') {
            steps {
                script {
                    sh '''
                        dotnet test "$TEST_PROJECT" \
                            --configuration Release \
                            --no-build \
                            --filter "Category=API" \
                            --logger "trx;LogFileName=api-results.trx" \
                            --results-directory ./TestResults/API
                    '''
                }
            }

            post {
                always {
                    archiveArtifacts(
                        artifacts: 'TestResults/API/**/*',
                        allowEmptyArchive: true
                    )
                }
            }
        }

        stage('UI Tests') {
            steps {
                script {

                    echo "Selected browser: ${params.BROWSER}"

                    withEnv(["BROWSER=${params.BROWSER}"]) {

                        sh '''
                            dotnet test "$TEST_PROJECT" \
                                --configuration Release \
                                --no-build \
                                --filter "Category=UI" \
                                --logger "trx;LogFileName=ui-results.trx" \
                                --results-directory ./TestResults/UI
                        '''
                    }
                }
            }

            post {
                always {

                    archiveArtifacts(
                        artifacts: 'TestResults/UI/**/*',
                        allowEmptyArchive: true
                    )

                    archiveArtifacts(
                        artifacts: 'TestResults/Screenshots/**/*',
                        allowEmptyArchive: true
                    )

                    archiveArtifacts(
                        artifacts: 'Screenshots/**/*',
                        allowEmptyArchive: true
                    )
                }
            }
        }
    }

    post {

        always {

            echo 'Publishing test results...'

            archiveArtifacts(
                artifacts: 'TestResults/**/*',
                allowEmptyArchive: true
            )

            junit(
                testResults: 'TestResults/**/*.trx',
                allowEmptyResults: true
            )
        }
    }
}