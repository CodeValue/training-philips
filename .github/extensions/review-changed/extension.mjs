// Dynamic workflow: review every changed C# file on this branch, in parallel, and write workflow-review.md.
// Code decides the steps. Agents only judge one diff each.
import { execSync } from "node:child_process";
import { writeFileSync } from "node:fs";
import { defineWorkflow, joinSession } from "@github/copilot-sdk/extension";

const text = (r) => (r == null ? "(agent failed)" : typeof r === "string" ? r : JSON.stringify(r, null, 2));

const reviewChanged = defineWorkflow({
    meta: {
        name: "review-changed",
        description:
            "Review each changed C# file on the current branch against a base branch, in parallel, " +
            "and write workflow-review.md. args: { base?: string } (default: main).",
        phases: [{ title: "Collect" }, { title: "Review" }, { title: "Report" }],
        argsSchema: { type: "object", properties: { base: { type: "string" } } },
        limits: { maxConcurrentSubagents: 3, maxTotalSubagents: 4, maxAiCredits: 60, timeoutSeconds: 600 },
    },
    run: async (ctx) => {
        const base = (ctx.args && ctx.args.base) || "main";

        ctx.phase("Collect");
        const files = await ctx.step("changed-files", () =>
            execSync(`git diff --name-only ${base}...HEAD -- "*.cs"`, { encoding: "utf8" })
                .split(/\r?\n/).filter(Boolean).slice(0, 4)          // never more than 4 agents
        );
        if (files.length === 0) return { files: 0, note: "No changed C# files." };

        ctx.phase("Review");
        const reviews = await ctx.parallel(files.map((file) => () => {
            const diff = execSync(`git diff ${base}...HEAD -- "${file}"`, { encoding: "utf8" }).slice(0, 12000);
            return ctx.agent(
                "You review ONE file's diff in a medical-imaging .NET app. " +
                "At most 3 findings, most serious first, each as: line · problem · smallest fix. " +
                "Look for: patient names in logs, responses or exports; swallowed exceptions; " +
                "copied formulas with a different constant; tests that can't fail. " +
                "If it's fine, answer exactly: No findings.\n\nFile: " + file + "\n\n" + diff,
                { label: `Review ${file}` }
            );
        }));

        ctx.phase("Report");
        const md = files.map((f, i) => `## ${f}\n\n${text(reviews[i])}`).join("\n\n");
        await ctx.step("write-report", () => {
            writeFileSync("workflow-review.md", `# Review of ${base}...HEAD\n\n${md}\n`);
            return { written: "workflow-review.md" };
        });
        ctx.log(`Wrote workflow-review.md for ${files.length} file(s). Run ${ctx.runId}`);
        return { files: files.length };
    },
});

await joinSession({ workflows: [reviewChanged] });
