using Inplanticular.CalculationService.Core.Exceptions;
using Inplanticular.CalculationService.Infrastructure.Calculations;
using Xunit;

namespace Calculation_Service.Infrastructure.Test;

public class GrowthCalculationTests {
	[Fact]
	public void CalculateRipePercentageToday_ShouldReturnDeadPlant_WhenNoWaterAnd7DaysNoWaterInRow() {
		// Arrange

		// Act + Assert
		Assert.Throws<PlantDeadException>(() => GrowthCalculation.CalculateRipePercentageToday(0, 0, 0, 7));
	}

	[Fact]
	public void CalculateRipePercentageToday_ShouldReturnNumber_WhenPlantedYesterdayAndGivenWater() {
		// Arrange

		// Act + Assert
		var ripePercentageToday = GrowthCalculation.CalculateRipePercentageToday(0, 1.5, 0, 0);
		Assert.Equal(1.5, ripePercentageToday);
	}

	[Fact]
	public void CalculateRipePercentageToday_ShouldReturnNumber_WhenPlantedYesterdayAndGivenWaterAndFertilizer() {
		var ripePercentageToday = GrowthCalculation.CalculateRipePercentageToday(0, 1.5, 0.5, 0);
		Assert.Equal(2, ripePercentageToday);
	}

	[Fact]
	public void CalculateRipeTime_ShouldReturnDays_WhenPlantedYesterdayAndGivenWaterAndFertilizer() {
		// Arrange
		var ripePercentageToday = GrowthCalculation.CalculateRipePercentageToday(10, 1.5, 0.5, 0);
		// Act
		var ripeTime = GrowthCalculation.CalculateRipeTime(1, ripePercentageToday);
		// Assert
		Assert.Equal(8, ripeTime);
	}

	[Fact]
	public void CalculateRipeTime_ShouldReturnDays_WhenPlanted16DaysAgoAndNotGivenWaterIn3DaysAndFertilizer() {
		// Arrange
		var ripePercentateToday = GrowthCalculation.CalculateRipePercentageToday(55, 1.5, 0.5, 3);
		// Act
		var ripeTime = GrowthCalculation.CalculateRipeTime(16, ripePercentateToday);
		// Assert
		Assert.Equal(28, ripeTime);
	}
}