using Inplanticular.CalculationService.Core.Exceptions;
using Inplanticular.CalculationService.Infrastructure.Calculations;
using Xunit;

namespace YieldCalculationService.Infrastructure.Test;

public class GrowthCalculationTests {
	[Fact]
	public void CalculateRipePercentageToday_ShouldReturnDeadPlant_WhenNoWaterAndSixDaysNoWaterInRow() {
		// Arrange

		// Act + Assert
		Assert.Throws<PlantDeadException>(() => GrowthCalculation.CalculateRipePercentageToday(0, 0, 0, true, 6));
	}

	[Fact]
	public void CalculateRipePercentageToday_ShouldReturnNumber_WhenPlantedYesterdayAndGivenWater() {
		// Arrange

		// Act + Assert
		var ripePercentateToday = GrowthCalculation.CalculateRipePercentageToday(0, 1.5, 0, false, 0);
		Assert.Equal(1.5, ripePercentateToday);
	}

	[Fact]
	public void CalculateRipePercentageToday_ShouldReturnNumber_WhenPlantedYesterdayAndGivenWaterAndFertilizer() {
		var ripePercentateToday = GrowthCalculation.CalculateRipePercentageToday(0, 1.5, 1.5, false, 0);
		Assert.Equal(3, ripePercentateToday);
	}

	[Fact]
	public void CalculateRipeTime_ShouldReturnDays_WhenPlantedYesterdayAndGivenWaterAndFertilizer() {
		// Arrange
		var ripePercentateToday = GrowthCalculation.CalculateRipePercentageToday(0, 1.5, 1.5, false, 0);
		// Act
		var ripeTime = GrowthCalculation.CalculateRipeTime(1, ripePercentateToday);
		// Assert
		Assert.Equal(33, ripeTime);
	}

	[Fact]
	public void CalculateRipeTime_ShouldReturnDays_WhenPlanted16DaysAgoAndNotGivenWaterIn3DaysAndFertilizer() {
		// Arrange
		var ripePercentateToday = GrowthCalculation.CalculateRipePercentageToday(55, 1.5, 1.5, true, 3);
		// Act
		var ripeTime = GrowthCalculation.CalculateRipeTime(16, ripePercentateToday);
		// Assert
		Assert.Equal(28, ripeTime);
	}
}