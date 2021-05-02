from random import random
from math import log as ln

class Exp:

    def __init__(self, theta):
        self.theta = theta

    def simulate(self):
        u = random()
        return "{:.2f}".format(-self.theta * ln(1 - u))
