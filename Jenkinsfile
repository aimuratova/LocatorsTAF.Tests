pipeline {

    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:10.0'
        }
    }

    parameters {
        choice(
            name: 'BROWSER',
            choices: ['Chrome', 'Firefox'],
            description: 'Browser to use for UI tests'
        )
    }

    triggers {
        cron('H 6 * * 1-5')
    }

    environment {
        TEST_PROJECT = 'LocatorsTAF.Tests/LocatorsTAF.Tests.csproj'
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Check .NET') {
            steps {
                sh '''
                    dotnet --info
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
                catchError(
                    buildResult: 'FAILURE',
                    stageResult: 'FAILURE'
                ) {
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
                catchError(
                    buildResult: 'FAILURE',
                    stageResult: 'FAILURE'
                ) {
                    withEnv(["BROWSER=${params.BROWSER}"]) {
                        sh '''
                            echo "Browser: $BROWSER"

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