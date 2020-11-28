import requests
from bs4 import BeautifulSoup as bs
import time
from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.firefox.options import Options

search = input("Enter Name => ")

mainUrl = f'https://www.pornpics.com/?q={search.replace(" ", "+")}&date=latest'

driverOption = webdriver.FirefoxOptions()
driverOption.add_argument('--headless')
driver = webdriver.Firefox(options=driverOption)  # options=driverOption
driver.get(mainUrl)
time.sleep(4)
# just choose a random element to send scroll down key
ele = driver.find_element_by_link_text("Live Cams")
for i in range(500):
    ele.send_keys(Keys.PAGE_DOWN)

driver.implicitly_wait(5)
source = driver.page_source
driver.stop_client()
driver.close()

soup = bs(source, 'html5lib')
a_links = []

with open(f'links - {search}.txt', "w", encoding="utf-8") as f:
    for link in soup.findAll('a', attrs={'class': 'rel-link'}):
        if 'pornpics' in link['href']:
            # a_links.append(link['href'])
            f.write(f'{link["href"]}+')

# for item in a_links:
#     print(f'{item}+')
