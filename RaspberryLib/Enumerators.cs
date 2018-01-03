namespace RaspberryLib
{
    public enum PinCode
    {
        NONE = 0,
        PIN1_3V3 = 1,
        PIN2_5V = 2,
        PIN3_GPIO02 = 3,
        PIN4_5V = 4,
        PIN5_GPIO03 = 5,
        PIN5_SDA1 = 5,
        PIN5_I2C = 5,
        PIN6_GND = 6,
        PIN7_GPIO04 = 7,
        PIN7_GPIOGCLK = 7,
        PIN8_GPIO14 = 8,
        PIN8_TXD0 = 8,
        PIN9_GND = 9,
        PIN10_GPIO15 = 10,
        PIN10_RXD0 = 10,
        PIN11_GPIO17 = 11,
        PIN11_GPIO_GEN0 = 11,
        PIN12_GPIO18 = 12,
        PIN12_GPIO_GEN1 = 12,
        PIN13_GPIO27 = 13,
        PIN13_GPIO_GEN2 = 13,
        PIN14_GND = 14,
        PIN15_GPIO22 = 15,
        PIN15_GPIO_GEN3 = 15,
        PIN16_GPIO23 = 16,
        PIN16_GPIO_GEN4 = 16,
        PIN17_3V3 = 17,
        PIN18_GPIO24 = 18,
        PIN18_GPIO_GEN5 = 18,
        PIN19_GPIO10 = 19,
        PIN19_SPI_MOSI = 19,
        PIN20_GND = 20,
        PIN21_GPIO09 = 21,
        PIN21_SPI_MISO = 21,
        PIN22_GPIO25 = 22,
        PIN22_GPIO_GEN6 = 22,
        PIN23_GPIO11 = 23,
        PIN23_SPICLK = 23,
        PIN24_GPIO08 = 24,
        PIN24_SPI_CE0_N = 24,
        PIN25_GND = 25,
        PIN26_GPIO27 = 26,
        PIN26_SPI_CE1_N = 26,
        PIN27_IDSD = 27,
        PIN27_I2C_ID_EPROM = 27,
        PIN28_IDSC = 28,
        PIN28_I2C_ID_EPROM = 28,
        PIN29_GPIO05 = 29,
        PIN30_GND = 30,
        PIN31_GPIO06 = 31,
        PIN32_GPIO12 = 32,
        PIN33_GPIO13 = 33,
        PIN34_GND = 34,
        PIN35_GPIO19 = 35,
        PIN36_GPIO16 = 36,
        PIN37_GPIO26 = 37,
        PIN38_GPIO20 = 38,
        PIN39_GND = 39,
        PIN40_GPIO21 = 40,
    }

    public enum PinDirectiion
    {
        UNKNOWN = -1,
        IN = 1,
        OUT = 0
    }

    public enum PinValue
    {
        UNKNOWN = -1,
        LOW = 0,
        HIGH = 1
    }
}