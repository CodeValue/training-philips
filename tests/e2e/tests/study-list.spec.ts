import { test, expect } from '@playwright/test';

test('study list shows the 8 synthetic studies', async ({ page }) => {
  await page.goto('/');

  const rows = page.locator('[data-testid^="study-row-"]');
  await expect(rows).toHaveCount(8);
});

test('clicking a study opens its detail panel', async ({ page }) => {
  await page.goto('/');

  await page.getByTestId('study-row-SYN-0002').click();

  await expect(page.locator('#study-detail')).toBeVisible();
  await expect(page.locator('#detail-title')).toHaveText('SYN-0002');
  await expect(page.getByTestId('analyze-Kidney')).toBeVisible();
});
