using AllAboutWeezer.Controllers;
using AllAboutWeezer.Models;
using Xunit;

namespace WeezerTests
{
    public class QuizTests
    {
        [Fact]
        public async Task TestLoadQuestions()
        {
            // Arrange
            var controller = new QuizController();
            var model = new WeezyTests();

            // Act
            var loadedModel = await controller.LoadQuestionsAsync(model);

            // Assert
            Assert.NotNull(loadedModel);
            Assert.NotNull(loadedModel.Questions);
            Assert.NotNull(loadedModel.Answers);
            Assert.NotEmpty(loadedModel.Questions);
            Assert.NotEmpty(loadedModel.Answers);
            Assert.Equal(controller.Questions, loadedModel.Questions);
            Assert.Equal(controller.Answers, loadedModel.Answers);
        }

        [Fact]
        public async Task TestCheckQuizAnswers()
        {
            // Arrange
            var model = new WeezyTests();
            var controller = new QuizController();
            var loadedModel = await controller.LoadQuestionsAsync(model);
            loadedModel.UserAnswers[1] = "Yes"; // Correct answer
            loadedModel.UserAnswers[2] = "No"; // Incorrect answer

            // Act
            var result = await controller.CheckQuizAnswersAsync(model);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Results);
            Assert.True(result.Results[1]); // First answer should be correct
            Assert.False(result.Results[2]); // Second answer should be incorrect
        }
    }
}
